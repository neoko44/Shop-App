using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Dtos;
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
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        IUserDal _userDal;
        

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserDal userDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userDal = userDal;
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                Address = userForRegisterDto.Address,
                City = userForRegisterDto.City,
                Phone = userForRegisterDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            
            _userService.Add(user);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }
        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.MailAlreadyExists);
            }
            return new SuccessResult();
        }
        public IDataResult<User> ChangePassword(string newPassword, string token)
        //giriş yapan kullanıcının şifresini değiştir
        {
            var tokenClaim = new JwtSecurityToken(jwtEncodedString: token);
            string email = tokenClaim.Claims.First(c => c.Type == "email").Value;
            var user = _userDal.Get(x => x.Email == email); //tokeni decrypt et . içerisinden mail kısmını bul
                                                            //bu mailin veri tabanında denk geldiği kullanıcıyı getir.
            byte[] passwordSalt, passwordHash;

            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);//eski şifreyi kontrol et

            if (user.PasswordHash == passwordHash)
            {
                return new ErrorDataResult<User>(Messages.NewPassMustDifferent);
            }
            else
            {
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;

                _userDal.Update(user);
            }
            return new SuccessDataResult<User>(Messages.PassChangeSuccess);
        }
    }
}
