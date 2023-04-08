using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service;
using MindITCommonModulesAPI.Service.Interface;

namespace MindITCommonModulesAPI.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    { 
            readonly IConfiguration configuration;
            readonly CustomExceptionHandler customExceptionHandler;
            public LoginController(IConfiguration configuration)
            {
                this.configuration = configuration;
                this.customExceptionHandler = new CustomExceptionHandler();
            }

        /// <summary>
        /// Verifying user authenticatiuon from db
        /// </summary>
        /// <param name="credentials">Username and Password</param>
        /// <returns>User Model class object</returns>
        /// <exception cref="CustomAPIExcpetion"></exception>
        [HttpPost("Login")]
        public IActionResult Login(LoginCredentials credentials)
        {
            try
            {
                IAuthService authService = new AuthService(configuration);
                ReferenceId user = authService.AuthenticateUser(credentials);
                return Ok(user);
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
        


        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string UserId, string Password)
        {
            try
            {
                ILoginService authService = new LoginService(configuration);
                bool verify = authService.ResetPassword(UserId, Password);

                return Ok(verify);


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
