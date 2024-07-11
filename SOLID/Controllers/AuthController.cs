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
    public class AuthController
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

        [HttpPost("/register")]
        public BaseResponse RegisterUser([FromBody] AuthRequest request)
        {
            try
            {
                _registerUser.Execute(request);

                return new BaseResponse { statusCode = System.Net.HttpStatusCode.Created, Message =  "User registered successfullt" };
            }
            catch (InvalidDataException ex)
            {
                return new BaseResponse { statusCode = System.Net.HttpStatusCode.BadRequest, Message = "User already exists" };
            }
        }

        [HttpPost("/login")]
        public LoginResponse AuthenticateUser([FromBody] AuthRequest request)
        {
            try
            {
                var response = _authenticateUser.Execute(request);

                return new LoginResponse { statusCode = System.Net.HttpStatusCode.OK, Token = response };
            }
            catch (Exception ex)
            {
                return new LoginResponse { statusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
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
