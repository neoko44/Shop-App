using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;
        private IProductDal _productDal;
        private IUserDal _userDal;

        public CartManager(ICartDal cartDal, IProductDal productDal, IUserDal userDal)
        {
            _cartDal = cartDal;
            _productDal = productDal;
            _userDal = userDal;
        }

        public IResult Add(Cart cart)
        {
            _cartDal.Add(cart);
            return new SuccessDataResult<Cart>(Messages.ProductAddedToCart);
        }

        public IResult Delete(Cart cart)
        {
            _cartDal.Delete(cart);
            return new SuccessDataResult<Cart>(Messages.ProductDeletedFromCart);
        }

        public IResult Update(Cart cart)
        {
            _cartDal.Update(cart);
            return new SuccessDataResult<Cart>(Messages.CartUpdated);
        }
    }
}
