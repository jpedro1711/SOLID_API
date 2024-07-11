using Microsoft.AspNetCore.Mvc;
using SOLID.Controllers.Requests;
using SOLID.Controllers.Responses;
using SOLID.Models;
using SOLID.Models.Enums;
using SOLID.UseCases.Interfaces;

namespace SOLID.Controllers
{
    public class AuthController
    {
        private readonly IRegisterUser _registerUser;
        private readonly IAuthenticateUser _authenticateUser;

        public AuthController(IRegisterUser registerUser, IAuthenticateUser authenticateUser)
        {
            _registerUser = registerUser;
            _authenticateUser = authenticateUser;
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
        public BaseResponse AuthenticateUser([FromBody] AuthRequest request)
        {
            try
            {
                var response = _authenticateUser.Execute(request);

                return new BaseResponse { statusCode = System.Net.HttpStatusCode.OK, Message = response };
            }
            catch (Exception ex)
            {
                return new BaseResponse { statusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }
    }
}
