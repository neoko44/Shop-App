using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(string firstName, string lastName, string email, string phone, string address, string city, string password);
        IDataResult<User> Login(string email, string password);
        IResult UserExists(string email);
        IDataResult<User> ChangePassword(string newPassword, string token);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
