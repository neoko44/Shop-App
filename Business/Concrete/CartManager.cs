using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;
        private IProductDal _productDal;
        private IUserDal _userDal;
        private IUserCartDal _userCartDal;


        public CartManager(ICartDal cartDal, IProductDal productDal, IUserDal userDal, IUserCartDal userCartDal)
        {
            _cartDal = cartDal;
            _productDal = productDal;
            _userDal = userDal;
            _userCartDal = userCartDal;
        }

        public IResult Add(int productId, int quantity, string token)
        {
            var tokenClaim = new JwtSecurityToken(jwtEncodedString: token);
            int userId = Convert.ToInt32(tokenClaim.Claims.First(c => c.Type == "nameid").Value);
            var user = _userDal.Get(x => x.Id == userId);//tokeni decrypt et ve tokendeki id'ye ait kullanıcıyı getir

            var Product = _productDal.Get(p => p.ProductId == productId);//kullanıcının girdiği productId değerine ait ürünü getir
            var UserCart = _userCartDal.Get(uc => uc.UserId == user.Id);
            var getCart = _cartDal.Get(c => c.UserId == user.Id && c.CartId == UserCart.Id && c.ProductId == productId);

            Cart cart = new Cart
            {
                ProductId = Product.ProductId,
                UserId = user.Id,
                Quantity = quantity,
                TotalPrice = quantity * Product.UnitPrice,
                CartId = UserCart.Id
            };


            if (getCart == null)
            {
                if (quantity > Product.UnitsInStock)
                {
                    return new ErrorDataResult<Cart>(Messages.InsufficientStock);
                }

                _cartDal.Add(cart);
                return new SuccessDataResult<Cart>(Messages.ProductAddedToCart);
            }



            else if (Product.ProductId != getCart.ProductId)//aynı ürün mü kontrol et
            {
                _cartDal.Add(cart);
                return new SuccessDataResult<Cart>(Messages.ProductAddedToCart);
            }


            else if (quantity + getCart.Quantity > Product.UnitsInStock)
                return new ErrorDataResult<Cart>(Messages.InsufficientStock);


            getCart.Quantity = quantity + getCart.Quantity;
            getCart.TotalPrice = getCart.Quantity * Product.UnitPrice;
            getCart.CartId = UserCart.Id;
            getCart.UserId = user.Id;
            getCart.ProductId = productId;


            _cartDal.Update(getCart);
            return new SuccessDataResult<Cart>(Messages.ProductAddedToCart);

        }

        public IResult Delete(int productId, int quantity, string token)
        {
            var tokenClaim = new JwtSecurityToken(jwtEncodedString: token);
            int userId = Convert.ToInt32(tokenClaim.Claims.First(c => c.Type == "nameid").Value);
            var user = _userDal.Get(x => x.Id == userId);//tokeni decrypt et ve tokendeki id'ye ait kullanıcıyı getir

            var Product = _productDal.Get(p => p.ProductId == productId);//kullanıcının girdiği productId değerine ait ürünü getir
            var UserCart = _userCartDal.Get(uc => uc.UserId == user.Id);
            var getCart = _cartDal.Get(c => c.UserId == user.Id && c.CartId == UserCart.Id && c.ProductId == productId);

            if (getCart == null)
            {
                return new ErrorDataResult<Cart>(Messages.ProductNotFound);
            }

            getCart.Quantity -= quantity;
            getCart.TotalPrice = getCart.Quantity * Product.UnitPrice;
            getCart.CartId = UserCart.Id;
            getCart.UserId = user.Id;
            getCart.ProductId = productId;

            _cartDal.Update(getCart);


            //if(getCart.Quantity == 0)
            //{

            //}
            return new SuccessDataResult<Cart>(Messages.ProductRemovedFromCart);

        }

        public IResult Update(Cart cart)
        {
            _cartDal.Update(cart);
            return new SuccessDataResult<Cart>(Messages.CartUpdated);
        }

        public IDataResult<Cart> GetById(int cartId)
        {
            return new SuccessDataResult<Cart>(_cartDal.Get(c => c.Id == cartId));

        }

        public IDataResult<Cart> GetByUserId(int userId)
        {
            return new SuccessDataResult<Cart>(_cartDal.Get(c => c.UserId == userId));
        }

        public IDataResult<Cart> GetByCartId(int cartId)
        {
            return new SuccessDataResult<Cart>(_cartDal.Get(c => c.CartId == cartId));
        }

        public IDataResult<List<ProductPreviewDto>> GetList(string token)
        {
            var tokenClaim = new JwtSecurityToken(jwtEncodedString: token);
            int userId = Convert.ToInt32(tokenClaim.Claims.First(c => c.Type == "nameid").Value);
            var user = _userDal.Get(x => x.Id == userId);//tokeni decrypt et ve tokendeki id'ye ait kullanıcıyı getir

            var getCart = _cartDal.GetList(x => x.UserId == user.Id).ToList();
            List<ProductPreviewDto> products = new();

            foreach (var product in getCart)
            {
                var prod = _productDal.Get(p => p.ProductId == product.ProductId);
                var prodDto = new ProductPreviewDto()
                {
                    Description = prod.Description,
                    ProductName = prod.ProductName,
                    Quantity = product.Quantity,
                    TotalPrice = product.TotalPrice
                };
                products.Add(prodDto);
            }

            if (products.IsNullOrEmpty())
            {
                return new ErrorDataResult<List<ProductPreviewDto>>(Messages.CartEmpty);
            }

            return new SuccessDataResult<List<ProductPreviewDto>>(products);

        }
    }
}
