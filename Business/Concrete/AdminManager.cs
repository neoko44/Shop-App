using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdminManager : IAdminService
    {
        private IUserDal _userDal;
        private IProductDal _productDal;
        private ICategoryDal _categoryDal;
        private ICartDal _cartDal;
        private IOrderDal _orderDal;
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IAdminService _adminService;

        public AdminManager(IUserDal userDal, IProductDal productDal, ICategoryDal categoryDal, ICartDal cartDal, IOrderDal orderDal, IUserService userService, ITokenHelper tokenHelper, IAdminService adminService)
        {
            _userDal = userDal;
            _productDal = productDal;
            _categoryDal = categoryDal;
            _cartDal = cartDal;
            _orderDal = orderDal;
            _userService = userService;
            _tokenHelper = tokenHelper;
            _adminService = adminService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
        public IResult Add(string firstName, string lastName,string password, string Email, string Phone, string Address, string City)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            User user = new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = Email,
                Phone = Phone,
                Address = Address,
                City = City,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 1002,
                Status = true
            };

            _userDal.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);

        }
    }
}
