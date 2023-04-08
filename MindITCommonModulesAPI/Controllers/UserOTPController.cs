using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service;
using MindITCommonModulesAPI.Service.Interface;
using MindITCommonModulesAPI.Util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MindITCommonModulesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOtpController : ControllerBase
    {

        private readonly IAuthService service;
        private readonly IConfiguration configuration;
        private readonly CustomExceptionHandler customExceptionHandler;
        public UserOtpController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.customExceptionHandler = new CustomExceptionHandler();
            service = new AuthService(configuration);
        }

        /// <summary>
        /// call UserService class Send method and Send OTP and save the OTP in the database
        /// Send OTP to verify the email 
        /// </summary>
        /// <param name="useremail">Is a object of UserEmail Model</param>
        /// <returns>Send Otp to the user email tpo verify their email Id </returns>
        [HttpPost("SendOTPVerification")]
        public IActionResult Send(Useremail useremail)
        {

            try
            {

                service.Sendotp(useremail);


            }
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());


                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };

            }
            return Ok(useremail);
        }
        /// <summary>
        /// call AuthService class method OTPverified and verify the OTP from database
        ///  IF OTP in invalid retun errorID 400 "Invalid OTP"
        /// </summary>
        /// <param name="userVerify">Is a object of UserVerify model</param>
        /// <returns>Get Otp from the database and verify</returns>
        [HttpPost("VerifyOTPVerification")]
        public IActionResult OTPRead(UserVerify userVerify)
        {
            try
            {
                service.OTPverified(userVerify);

            }
            catch (CustomAPIExcpetion ex)
            {
                CustomExceptionResponse exceptionResponse = customExceptionHandler.handleCustomException(ex.getError());


                return new ObjectResult(exceptionResponse.errorResponse)
                {
                    StatusCode = (int)exceptionResponse.httpStatusCode
                };

            }
            return Ok(userVerify);
        }
    }
}
