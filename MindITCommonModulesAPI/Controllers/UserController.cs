using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service;
using MindITCommonModulesAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MindITCommonModulesAPI.Controllers
{
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IAuthService service;
        private readonly IConfiguration configuration;
        private readonly CustomExceptionHandler customExceptionHandler;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.customExceptionHandler = new CustomExceptionHandler();
            service = new AuthService(configuration);
        }




        // Forgot Password Verify 
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPassword forgotusername)
        {
            try
            {
                IUserService userService = new UserService(configuration);
                ReferenceId forgot = userService.ForgotPassword(forgotusername);
                return Ok(forgot);
            }
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());

                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };

            }

        }
        /// <summary>
        ///  Call  AuthService class to get all non-deleted users details 
        /// IF List Empty return ErrorID 204 No content "Empty List"
        /// If List Not Empty Return Success Code 200
        /// </summary>
        /// <returns>Get all non-deleted users list</returns>
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<UserModel> usersList = new List<UserModel>();
            try
            {

                usersList = service.GetUserList();
                return Ok(usersList);

            }
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());


                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };

            }
        }
        /// <summary>
        /// call Authservice class and Update the user details
        /// IF firstName and lastName is null retun error Id 400 "Invalid Name"
        /// IF Success return Success code 200
        /// </summary>
        /// <param name="user">Object of UserModel </param>
        /// <returns>Update user Details as per permision "firstName" & "lastName"</returns>
        [HttpPut("UpdateUser")]
        public IActionResult Update(UserModel user)
        {
            UserModel userModel = new UserModel();
            try
            {
                userModel = service.UpdateUserDetails(user);
                return Ok(userModel);


            }
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());


                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };

            }
        }
    }
}
