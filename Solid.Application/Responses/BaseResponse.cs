using System.Net;

namespace SOLID.Controllers.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode statusCode;
        public string Message { get; set; }
    }
}
