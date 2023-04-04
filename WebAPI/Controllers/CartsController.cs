using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        ICartService _cartService;
        IUserDal _userDal;
        public CartsController(ICartService cartService, IUserDal userDal)
        {
            _cartService = cartService;
            _userDal = userDal;
        }

        [HttpGet("addtocart")]
        public IActionResult AddToCart(int productId, string token)
        {
            var tokenClaim = new JwtSecurityToken(jwtEncodedString: token);
            string Id = tokenClaim.Claims.First(c => c.Type == "nameid").Value;
            int userId = Convert.ToInt32(Id);
            var user = _userDal.Get(x => x.Id == userId);

            Cart cart = new Cart
            {
                ProductId = productId,
                UserId = user.Id,
            };

            var result = _cartService.Add(cart);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);

        }
    }
}
