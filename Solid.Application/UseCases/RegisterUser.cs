using SOLID.Models;
using SOLID.Repositories.Interfaces;
using SOLID.UseCases.Interfaces;
using SOLID.Controllers.Requests;
using SOLID.Models.Enums;
using SOLID.Repositories;

namespace SOLID.UseCases
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public RegisterUser(IUserRepository userRepository, IEmployeeRepository employeeRepository)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
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

            var employee = new Employee { Category = "hourly", Name = request.Username, HourlyRate = 50 };   

            _employeeRepository.Save(employee);
            // Save unique user
            _userRepository.Save(user);
        }
    }
}
