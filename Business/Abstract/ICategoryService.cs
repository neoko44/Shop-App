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
        IDataResult<List<CategoryDto>> GetList();
        IDataResult<List<Product>> GetAllLists(int productId);
        IDataResult<List<Category>> GetByName (string categoryName);
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);
    }
}
