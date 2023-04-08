using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util;

namespace MindITCommonModulesAPI.ExceptionHandler
{
    public class CustomExceptionHandler
    {

        public CustomExceptionResponse handleCustomException(CustomErrorCodeEnum ex)
        {
            CustomErrorResponse errorResponse = new CustomErrorResponse();
            errorResponse.errorMessage = EnumUtilities.GetErrorMessage(ex);
            errorResponse.errorCode = EnumUtilities.GetErrorCode(ex);
            errorResponse.errorId = EnumUtilities.GetErrorId(ex);
            errorResponse.errorSeverity = EnumUtilities.GetErrorSeverity(ex);
            CustomExceptionResponse customExceptionResponse = new CustomExceptionResponse();
            customExceptionResponse.errorResponse=errorResponse;
            customExceptionResponse.httpStatusCode = EnumUtilities.GetHttpStatus(ex);
            return customExceptionResponse;
           
        }
    }
}
