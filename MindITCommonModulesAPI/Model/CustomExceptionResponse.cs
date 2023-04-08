using System.Net;

namespace MindITCommonModulesAPI.Model
{
    public class CustomExceptionResponse
    {
        public CustomErrorResponse? errorResponse { get; set; }
        public HttpStatusCode httpStatusCode { get; set; }
    }
}
