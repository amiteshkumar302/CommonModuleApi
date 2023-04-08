using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service.Interface;
using MindITCommonModulesAPI.Util;

namespace MindITCommonModulesAPI.Service
{
    public class UserService:IUserService
    {
        private readonly IConfiguration configuration;

        public UserService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Authenticating Forgot Username 
        /// </summary>
        /// <param name="forgotUsername"> Username</param>
        /// <returns>Reference ID </returns>
        public ReferenceId ForgotPassword(ForgotPassword forgotUsername)
        {
            string? otpsend = null;
            Random r = new Random();
            string otp = r.Next(10001, 99999).ToString();
            string  factory = TwoFactorySetup.GetTwoFactory();
            string refid = null;
            
            ReferenceId RefId = new ReferenceId();
            try
            {
                if (forgotUsername.Username == null)
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.NULL_PARAMETER);
                }
                else
                {
                    UserDA userDA = new UserDA(configuration);
                    OtpDA otpDA = new OtpDA(configuration);
                    Login? forgotPassUsername = userDA.ForgotPassword(forgotUsername);

                    if (forgotPassUsername != null)
                    {
                        if (forgotPassUsername.Type == TYPEEnum.MobileNumber.ToString() && factory == "True")
                        {
                            IOTPService otpService = new OtpService(configuration);
                             otpsend = otpService.MobileOtp(forgotPassUsername.MobileNumber,otp);

                           

                        }
                        else if (forgotPassUsername.Type == TYPEEnum.Email.ToString() && factory == "True")
                        {

                        }
                        else if (forgotPassUsername.Type == TYPEEnum.Username.ToString())
                        {
                            IOTPService otpService = new OtpService(configuration);
                            otpsend = otpService.MobileOtp(forgotPassUsername.MobileNumber, otp);
                            // Email OTP
                        }

                        forgotPassUsername.OTP = otp;
                        // forgot opt save
                        refid = otpDA.SaveOTP(forgotPassUsername);
                        RefId.RefID = refid;
                        return RefId;
                    }
                    else
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_CREDENTIALS);

                }
            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }


        }

    }
}
