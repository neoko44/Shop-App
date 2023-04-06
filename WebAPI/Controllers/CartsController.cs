using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        ICartService _cartService;
        IProductService _productService;
        public CartsController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [HttpPost("addtocart")]
        public IActionResult AddToCart(int productId, short quantity, string token)
        {
            var result = _cartService.Add(productId, quantity, token);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getcart")]
        public IActionResult GetCart(string token)
        {
            var result = _cartService.GetList(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("removefromcart")]
        public IActionResult RemoveFromCart(int productId, short quantity, string token)
        {
            var result = _cartService.Delete(productId, quantity, token);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
