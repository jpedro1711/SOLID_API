using SOLID.Models;
using SOLID.Repositories.Interfaces;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using SOLID.UseCases.Interfaces;
using SOLID.Controllers.Requests;
using SOLID.Models.Enums;

namespace SOLID.UseCases
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IUserRepository _userRepository;

        public RegisterUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(AuthRequest request)
        {
            var user = new User { Username = request.Username, Password = request.Password, Role = UserRole.EMPLOYEE };
            // verify if user exists

            var userExists = _userRepository.Get(u => u.Username.ToLower() == user.Username.ToLower());

            if (userExists.Any())
            {
                throw new InvalidDataException("User exists with username");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = passwordHash;

            // Save unique user
            _userRepository.Save(user);
        }
    }
}
