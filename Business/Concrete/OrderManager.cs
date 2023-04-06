using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Business.Concrete
{

    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;
        private IUserDal _userDal;
        private IUserCartDal _userCartDal;
        private ICartDal _cartDal;
        private IProductDal _productDal;
        private IWalletDal _walletDal;


        public OrderManager(IOrderDal orderDal, IUserDal userDal, IUserCartDal userCartDal, ICartDal cartDal, IProductDal productDal, IWalletDal walletDal)
        {
            _orderDal = orderDal;
            _userDal = userDal;
            _userCartDal = userCartDal;
            _cartDal = cartDal;
            _productDal = productDal;
            _walletDal = walletDal;
        }

        public IResult Add(string token)
        {
            var tokenClaim = new JwtSecurityToken(jwtEncodedString: token);
            string email = tokenClaim.Claims.First(c => c.Type == "email").Value;
            var user = _userDal.Get(x => x.Email == email); //tokeni decrypt et . içerisinden mail kısmını bul
                                                            //bu mailin veri tabanında denk geldiği kullanıcıyı getir.

            var getUserCart = _userCartDal.Get(uc => uc.UserId == user.Id && uc.IsOrder == false);
            var getCartList = _cartDal.GetList(c => c.UserId == user.Id && c.CartId == getUserCart.Id && c.IsOrder == false).ToList();
            if (getCartList == null)
            {
                return new ErrorDataResult<Order>(Messages.CartEmpty);
            }
            var orderId = CreateOrderId();
            var getWallet = _walletDal.Get(w => w.UserId == user.Id);

            var check = _orderDal.Get(o => o.CartId == getUserCart.Id);

            if (check != null)
            {
                return new ErrorDataResult<Order>(Messages.OrderAlreadyExists);
            }


            decimal totalPrice = 0.00m;

            foreach (var cart in getCartList)
            {
                totalPrice += cart.TotalPrice;
            }

            Order order = new Order()
            {
                Description = "Siparişiniz Oluşturuldu",
                Freight = 34.99m,
                OrderDate = DateTime.Now,
                ShippedDate = DateTime.Now.AddHours(8),
                Status = true,
                UserId = user.Id,
                TotalPrice = totalPrice,
                CartId = getUserCart.Id,
                OrderId = orderId
            };

            var orderTotalPriceAndFreight = order.Freight + order.TotalPrice;
            if (orderTotalPriceAndFreight > getWallet.Balance)
            {
                return new ErrorDataResult<Order>(Messages.NotEnoughBalance);
            }

            getWallet.Balance -= orderTotalPriceAndFreight;
            _walletDal.Update(getWallet);

            getUserCart.IsOrder = true;
            _userCartDal.Update(getUserCart);
            _orderDal.Add(order); //siparişi oluştur


            foreach (var cartProduct in getCartList)
            {
                var getProduct = _productDal.Get(p => p.ProductId == cartProduct.ProductId);
                getProduct.UnitsInStock -= cartProduct.Quantity;
                getProduct.UnitsOnOrder += cartProduct.Quantity;
                if (getProduct.UnitsInStock == 0)
                {
                    getProduct.Discontinued = true;
                }

                cartProduct.IsOrder = true;
                _cartDal.Update(cartProduct);
                _productDal.Update(getProduct);
            }
            //ürünlerin stoklarını düzenle


            return new SuccessDataResult<Order>(Messages.OrderCreated);
        }
        public IDataResult<List<OrderDto>> GetList(string token)
        {
            var tokenClaim = new JwtSecurityToken(jwtEncodedString: token);
            string email = tokenClaim.Claims.First(c => c.Type == "email").Value;
            var user = _userDal.Get(x => x.Email == email); //tokeni decrypt et . içerisinden mail kısmını bul
                                                            //bu mailin veri tabanında denk geldiği kullanıcıyı getir.
            var getUserCarts = _userCartDal.GetList(uc => uc.UserId == user.Id && uc.IsOrder == true);

            List<Cart> allCartsForOrder = new();
            List<CartProductDto> cartProductDtos = new();

            foreach (var userCart in getUserCarts)
            {
                var carts = _cartDal.GetList(c => c.UserId == user.Id && c.CartId == userCart.Id).ToList();
                foreach (var cart in carts)
                {
                    allCartsForOrder.Add(cart);
                }
            }

            UserDto userDto = new UserDto()
            {
                Address = user.Address,
                City = user.City,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,

            };

            var getOrders = _orderDal.GetList(o => o.UserId == user.Id && o.Status == true).ToList();
            if (getOrders.IsNullOrEmpty())
            {
                return new ErrorDataResult<List<OrderDto>>(Messages.NoOrder);
            }
            foreach (var product in allCartsForOrder)
            {
                var getProduct = _productDal.Get(p => p.ProductId == product.ProductId);
                CartProductDto cartProductDto = new CartProductDto()
                {
                    ProductName = getProduct.ProductName,
                    Quantity = product.Quantity,
                    TotalPrice = product.TotalPrice,
                    UnitPrice = product.TotalPrice / product.Quantity
                };
                cartProductDtos.Add(cartProductDto);
            }

            List<OrderDto> ordersDto = new List<OrderDto>();
            foreach (var order in getOrders)
            {
                OrderDto orderDto = new OrderDto
                {
                    Description = order.Description,
                    Freight = order.Freight,
                    OrderDate = order.OrderDate,
                    OrderId = order.OrderId,
                    ShippedDate = order.ShippedDate,
                    TotalPrice = order.TotalPrice,
                    ProductInfo = cartProductDtos,
                    UserInfo = userDto
                };
                ordersDto.Add(orderDto);
            }
            return new SuccessDataResult<List<OrderDto>>(ordersDto);
        }
        public int CreateOrderId()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            int orderId = randomNumber;


            return orderId;
        }

        public IDataResult<OrderDto> GetOrderById(int orderId)
        {
            
            var order = _orderDal.Get(o => o.OrderId == orderId);
            if (order == null)
            {
                return new ErrorDataResult<OrderDto>(Messages.NoOrder);
            }

            var user = _userDal.Get(u => u.Id == order.UserId);

            UserDto userDto = new UserDto()
            {
                Address = user.Address,
                City = user.City,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,

            };


            List<CartProductDto> cartProductDtos = new();
            List<Cart> allCartsForOrder = new();
            var carts = _cartDal.GetList(c => c.CartId == order.CartId).ToList();

            foreach (var cart in carts)
            {
                allCartsForOrder.Add(cart);
            }

            foreach (var cart in allCartsForOrder)
            {
                var getProduct = _productDal.Get(p => p.ProductId == cart.ProductId);
                CartProductDto cartProductDto = new()
                {
                    ProductName = getProduct.ProductName,
                    Quantity = cart.Quantity,
                    TotalPrice = cart.TotalPrice,
                    UnitPrice = getProduct.UnitPrice
                };
                cartProductDtos.Add(cartProductDto);
            }


            OrderDto orderDto = new()
            {
                Description = order.Description,
                Freight = order.Freight,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                ShippedDate = order.ShippedDate,
                TotalPrice = order.TotalPrice,
                ProductInfo = cartProductDtos,
                UserInfo = userDto
            };

            return new SuccessDataResult<OrderDto>(orderDto);

        }
    }
}
