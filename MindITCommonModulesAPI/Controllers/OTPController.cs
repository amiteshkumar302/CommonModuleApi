using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service;
using MindITCommonModulesAPI.Service.Interface;

namespace MindITCommonModulesAPI.Controllers
{
    [ApiController]
    public class OTPController : Controller
    {
        readonly IConfiguration configuration;
        readonly CustomExceptionHandler customExceptionHandler;
        public OTPController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.customExceptionHandler = new CustomExceptionHandler();
        }

        /// <summary>
        /// Verifying Login OTP from Database
        /// </summary>
        /// <param name="Refid"> Reference Id generated after OTP Generation</param>
        /// <param name="otp">OTP Inputed by user</param>
        /// <returns>Token is generated</returns>
        [HttpPost("Verify")]
        public IActionResult Verify(string Refid, string otp)
        {
            try
            {
                IOTPService authService = new OtpService(configuration);
                Token verify = authService.Loginverification(Refid, otp);

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
        /// <summary>
        /// Verifying Forget OTP from Database
        /// </summary>
        /// <param name="Refid"> Reference Id generated after OTP Generation</param>
        /// <param name="otp">OTP Inputed by user</param>
        /// <returns>Token is generated</returns>
        [HttpPost("ForgetVerify")]
        public IActionResult ForgetVerify(string Refid, string otp)
        {
            try
            {
                IOTPService authService = new OtpService(configuration);
                Token verify = authService.forgetverification(Refid, otp);

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
