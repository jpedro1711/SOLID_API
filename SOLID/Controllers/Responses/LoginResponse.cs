using Microsoft.AspNetCore.Http;
using System.Net;

namespace SOLID.Controllers.Responses
{
    public class LoginResponse
    {
        public HttpStatusCode statusCode;
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
