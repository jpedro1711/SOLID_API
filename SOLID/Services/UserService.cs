using SOLID.Controllers.Responses;
using SOLID.Models.Enums;
using SOLID.Services.interfaces;
using System.Security.Claims;

namespace SOLID.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserDataResponse GetUserData()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                var role = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                var username = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                var roleEnum = (UserRole)Enum.Parse(typeof(UserRole), role, true);

                return new UserDataResponse { Username = username, Role = Enum.GetName(typeof(UserRole), roleEnum) };
            }

            return null;
        }
    }
}
