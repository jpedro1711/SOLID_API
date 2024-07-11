using SOLID.Models;

namespace SOLID.Services.interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
