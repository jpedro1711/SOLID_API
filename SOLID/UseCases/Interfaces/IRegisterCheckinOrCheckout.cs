using SOLID.Controllers.Requests;
using SOLID.Controllers.Responses;
using SOLID.Models;

namespace SOLID.UseCases.Interfaces
{
    public interface IRegisterCheckinOrCheckout
    {
        BaseResponse Execute(RegisterCheckinOrCheckoutRequest request);
    }
}
