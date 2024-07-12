using SOLID.Controllers.Requests;
using SOLID.Controllers.Responses;
using SOLID.Models;
using SOLID.Models.Enums;
using SOLID.Repositories.Interfaces;
using SOLID.Services;
using SOLID.Services.interfaces;
using SOLID.UseCases.Interfaces;

namespace SOLID.UseCases
{
    public class AuthenticateUser : IAuthenticateUser
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthenticateUser(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public string Execute(AuthRequest request)
        {
            var user = new User { Username = request.Username, Password = request.Password, Role = UserRole.EMPLOYEE };
            var userExists = _userRepository.Get(x => x.Username.ToLower() == user.Username.ToLower());  

            if (!userExists.Any())
            {
                throw new Exception("Incorrect username");
            }

            if (!BCrypt.Net.BCrypt.Verify(user.Password, userExists.First().Password))
            {
                throw new Exception("Incorrect password");
            }

            string token = _tokenService.CreateToken(user);

            return token;
        }
    }
}
