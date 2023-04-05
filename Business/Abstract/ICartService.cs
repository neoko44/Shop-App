using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        IResult Add(int productId,int quantity,string token);
        IResult Update(Cart cart);
        IResult Delete(Cart cart);
        IDataResult<List<ProductPreviewDto>> GetList(string token);
        IDataResult<Cart> GetById(int cartId);
        IDataResult<Cart> GetByUserId(int userId);
        IDataResult<Cart> GetByCartId(int cartId);
    }
}
