using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserCartService
    {
        IResult Add(UserCart userCart);
        IDataResult<UserCart> GetByUserId(int userId);
    }
}
