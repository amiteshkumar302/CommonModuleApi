using System.Net;
using System.Reflection;

namespace MindITCommonModulesAPI.ExceptionHandler
{
    public  enum CustomErrorCodeEnum
    {
        [CustomErrorCode(ErrorId = 10001, ErrorCode = "INTERNAL_SERVER_ERROR", ErrorMessage = "Please reach out to Support", HttpStatus = HttpStatusCode.InternalServerError, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        INTERNAL_SERVER_ERROR,
        [CustomErrorCode(ErrorId = 10002, ErrorCode = "INVALID_CREDENTIALS", ErrorMessage = "Invalid user credentials", HttpStatus = HttpStatusCode.Unauthorized, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        INVALID_CREDENTIALS,
        [CustomErrorCode(ErrorId = 10003, ErrorCode = "BAD_REQUEST", ErrorMessage = "Role Type is not found", HttpStatus = HttpStatusCode.BadRequest, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        BAD_REQUEST,
        [CustomErrorCode(ErrorId = 10004, ErrorCode = "BAD_REQUEST", ErrorMessage = "Active/Inactive status is not valid", HttpStatus = HttpStatusCode.BadRequest, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        BAD_STATUS_REQUEST,
        [CustomErrorCode(ErrorId = 10005, ErrorCode = "CONFLICT", ErrorMessage = "Role Type is already exists", HttpStatus = HttpStatusCode.Conflict, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        CONFLICT,
        [CustomErrorCode(ErrorId = 10006, ErrorCode = "NOT_FOUND", ErrorMessage = "Role ID not found", HttpStatus = HttpStatusCode.NotFound, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        NOT_FOUND,
        [CustomErrorCode(ErrorId = 10007, ErrorCode = "METHOD_NOT_ALLOWED", ErrorMessage = "Users found of this roletype", HttpStatus = HttpStatusCode.MethodNotAllowed, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        METHOD_NOT_ALLOWED,
        [CustomErrorCode(ErrorId = 10008, ErrorCode = "NO CONTENT", ErrorMessage = "Not any role Found", HttpStatus = HttpStatusCode.NoContent, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        No_Role,
        [CustomErrorCode(ErrorId = 10009, ErrorCode = "MISMATCH", ErrorMessage = "Role ID Mismatch", HttpStatus = HttpStatusCode.MethodNotAllowed, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        MISMATCH,
        [CustomErrorCode(ErrorId = 10010, ErrorCode = "NO_CONTENT", ErrorMessage = "Not any role type found", HttpStatus = HttpStatusCode.BadRequest, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        INVALID_VALUE,
        [CustomErrorCode(ErrorId = 10011, ErrorCode = "NULL_PARAMETER", ErrorMessage = "Parameter cannot be null", HttpStatus = HttpStatusCode.NoContent, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        NULL_PARAMETER,
        [CustomErrorCode(ErrorId = 204, ErrorCode = "NO_CONTENT", ErrorMessage = "Empty User Detail", HttpStatus = HttpStatusCode.NoContent, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        NO_CONTENT,
        [CustomErrorCode(ErrorId = 400, ErrorCode = "INVALID_OTP_ReferenceID", ErrorMessage = "Please Enter The Valid OTP", HttpStatus = HttpStatusCode.BadRequest, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        INVALID_OTP_ReferenceID,
        [CustomErrorCode(ErrorId = 400, ErrorCode = "INVALID_EmailAddress", ErrorMessage = "Please Enter The Valid Email", HttpStatus = HttpStatusCode.BadRequest, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        INVALID_EmailAddress,
        [CustomErrorCode(ErrorId = 400, ErrorCode = "INVALID_Name", ErrorMessage = "Please Enter The Name", HttpStatus = HttpStatusCode.BadRequest, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        INVALID_Name,

        //[CustomErrorCode(ErrorId = 10012, ErrorCode = "NO_CONTENT", ErrorMessage = "Data Not Found", HttpStatus = HttpStatusCode.NoContent, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        //NO_CONTENT,
        [CustomErrorCode(ErrorId = 10012, ErrorCode = "NOT_FOUND", ErrorMessage = "Data not found", HttpStatus = HttpStatusCode.NotFound, ErrorSeverity = ErrorSeverityEnum.ERROR)]
        NO_DATA,
    }
    #region [ CustomErrorCodeAttribute ]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CustomErrorCodeAttribute : Attribute
    {
        public int ErrorId { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public HttpStatusCode HttpStatus { get; set; }
        public ErrorSeverityEnum ErrorSeverity { get; set; }
        
    }
    #endregion


}
