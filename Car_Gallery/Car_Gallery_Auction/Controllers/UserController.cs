using Car_Gallery_Auction.Business.Abstract;
using Car_Gallery_Auction.Business.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Gallery_Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;

        }


        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody]RegisterRequestDto model)
        {
            var response = await _userService.Register(model);

            if (response.isSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);


        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto model)
        {
            var response = await _userService.Login(model);

            if (response.isSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);


        }

    }
}
