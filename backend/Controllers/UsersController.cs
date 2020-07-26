using backend.Helpers;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace backend.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserLoginService _userService;

        public UsersController(IUserLoginService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="model"></param>  
        /// <response code="200">Returns user Login information with access token</response>
        /// <response code="400">If the item is null or the Username or password is incorrect</response>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        /// <summary>
        /// Initial creation of three users in the database.
        /// </summary> 
        /// <response code="200">Returns user Login information with access token</response>
        [HttpPost("createusers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateUsers()
        {
            var users = _userService.CreateUsers();
            return Ok(users);
        }
    }
}