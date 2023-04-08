using MindITCommonModulesAPI.ExceptionHandler;

namespace MindITCommonModulesAPI.Model
{
    public class CustomErrorResponse
    {

        public int errorId { get; set; }
        public string? errorCode { get; set; }
        public string? errorMessage { get; set; }
        public ErrorSeverityEnum? errorSeverity { get; set; }

    }
}
