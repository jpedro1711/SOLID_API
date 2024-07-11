using SOLID.Controllers.Requests;
using SOLID.Models;

namespace SOLID.UseCases.Interfaces
{
    public interface IRegisterUser
    {
        void Execute(AuthRequest request);
    }
}
