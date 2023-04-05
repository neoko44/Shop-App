using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserCartManager : IUserCartService
    {
        IUserCartDal _userCartDal;

        public UserCartManager(IUserCartDal userCartDal)
        {
            _userCartDal = userCartDal;
        }

        public IResult Add(UserCart userCart)
        {
            _userCartDal.Add(userCart);
            return new SuccessDataResult<UserCart>(Messages.UserCartCreated);
        }

        public IDataResult<UserCart> GetByUserId(int userId)
        {
            return new SuccessDataResult<UserCart>(_userCartDal.Get(c => c.UserId == userId && c.IsOrder == false));
        }
    }
}
