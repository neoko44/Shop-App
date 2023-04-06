using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IUserDal _userDal;
        private IProductDal _productDal;
        private ICategoryDal _categoryDal;
        private ICartDal _cartDal;
        private IOrderDal _orderDal;
        private IAdminService _adminService;

        private IUserService _userService;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IOrderService _orderService;
        private ICartService _cartService;

        public AdminController(IUserDal userDal, IProductDal productDal, ICategoryDal categoryDal, ICartDal cartDal, IOrderDal orderDal, IUserService userService, IProductService productService, ICategoryService categoryService, IOrderService orderService, ICartService cartService)
        {
            _userDal = userDal;
            _productDal = productDal;
            _categoryDal = categoryDal;
            _cartDal = cartDal;
            _orderDal = orderDal;
            _userService = userService;
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _cartService = cartService;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("adduser")]
        public IActionResult AddUser(string token, string firstName, string lastName, string Email, string password, string Phone, string Address, string City)
        {
            var result = _adminService.Add(token, firstName, lastName, Email, password, Phone, Address, City);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result.Message);
        }
    }
}
