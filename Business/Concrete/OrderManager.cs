using Business.Abstract;
using Business.Constants;
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

        public OrderManager(IOrderDal orderDal, IUserDal userDal, IUserCartDal userCartDal, ICartDal cartDal)
        {
            _orderDal = orderDal;
            _userDal = userDal;
            _userCartDal = userCartDal;
            _cartDal = cartDal;
        }

        public IResult Add(string token)
        {
            var tokenClaim = new JwtSecurityToken(jwtEncodedString: token);
            string email = tokenClaim.Claims.First(c => c.Type == "email").Value;
            var user = _userDal.Get(x => x.Email == email); //tokeni decrypt et . içerisinden mail kısmını bul
                                                            //bu mailin veri tabanında denk geldiği kullanıcıyı getir.

            var getUserCart = _userCartDal.Get(uc => uc.UserId == user.Id);
            var getCartList = _cartDal.GetList(c => c.UserId == user.Id && c.CartId == getUserCart.Id).ToList();
            decimal totalPrice = 0.00m;

            foreach (var cart in getCartList)
            {
                totalPrice += cart.TotalPrice;
            }

            Order order = new Order()
            {
                Description = "On Ship",
                Freight = 34.99m,
                OrderDate = DateTime.Now,
                ShippedDate = DateTime.Now.AddHours(8),
                Status = true,
                UserId = user.Id,
                TotalPrice = totalPrice
            };
            _orderDal.Add(order);
            return new SuccessDataResult<Order>(Messages.OrderCreated);
        }
    }
}
