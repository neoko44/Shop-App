using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("getproductsbycategoryid")]
        public IActionResult GetListByCategoryId(int categoryId) 
        { 
            var result = _productService.GetListByCategory(categoryId);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getproductbyname")]
        public IActionResult GetProductByName(string productName) 
        {
            var result = _productService.GetProductByName(productName);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        
    }
}
