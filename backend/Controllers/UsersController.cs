using backend.Helpers;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserLoginService _userService;

        public UsersController(IUserLoginService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

     
        [HttpPost("createusers")]
        public IActionResult CreateUsers()
        {
            var users = _userService.CreateUsers();
            return Ok(users);
        }
    }
}