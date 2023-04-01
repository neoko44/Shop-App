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
    public interface ICategoryService
    {
        IDataResult<Category> GetById(int categoryId);
        IDataResult<List<Category>> GetList();
        IDataResult<List<Product>> GetAllLists(int productId);
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);
    }
}
