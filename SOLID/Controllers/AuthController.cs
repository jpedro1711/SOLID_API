using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOLID.Controllers.Requests;
using SOLID.Controllers.Responses;
using SOLID.Models;
using SOLID.Models.Enums;
using SOLID.Services.interfaces;
using SOLID.UseCases.Interfaces;

namespace SOLID.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterUser _registerUser;
        private readonly IAuthenticateUser _authenticateUser;
        private readonly IUserService _userService;

        public AuthController(IRegisterUser registerUser, IAuthenticateUser authenticateUser, IUserService userService)
        {
            _registerUser = registerUser;
            _authenticateUser = authenticateUser;
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] AuthRequest request)
        {
            try
            {
                _registerUser.Execute(request);

                return Ok(new BaseResponse { statusCode = System.Net.HttpStatusCode.Created, Message =  "User registered successfullt" });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse { statusCode = System.Net.HttpStatusCode.BadRequest, Message = "User already exists" });
            }
        }

        [HttpPost("login")]
        public IActionResult AuthenticateUser([FromBody] AuthRequest request)
        {
            try
            {
                var response = _authenticateUser.Execute(request);
                return Ok(new LoginResponse { statusCode = System.Net.HttpStatusCode.OK, Token = response });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

                
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public UserDataResponse GetCurrentLoggedInUser()
        {
            return _userService.GetUserData();
        }
    }
}
