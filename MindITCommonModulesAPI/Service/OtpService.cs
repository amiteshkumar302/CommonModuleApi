using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service.Interface;
using MindITCommonModulesAPI.Util;
using MindITCommonModulesAPI.Util.StoredProcedures;
using MindITCommonModulesAPI.Util.TwilioSetup;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MindITCommonModulesAPI.Service
{
    public class OtpService: IOTPService
    {

        private readonly TokenUtil tokenUtil;
        private readonly IConfiguration configuration;

        public OtpService(IConfiguration configuration)
        {
            this.configuration = configuration;
            tokenUtil = new TokenUtil(configuration);


        }




        /// <summary>
        /// Method for Login OTP Veirfication
        /// </summary>
        /// <param name="ref_id"></param>
        /// <param name="otp"></param>
        /// <returns>Generated Token</returns>
        public Token Loginverification(string ref_id, string otp)
        {
            try
            {
                if (ref_id == null || otp == null)
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.NULL_PARAMETER);
                }
                else
                {
                    Token token = new Token();
                    OtpDA otpDA = new OtpDA(configuration);
                    bool user = otpDA.Verifyotp(ref_id, otp, Parameter.Login);
                    if (user)
                    {
                        string accessToken = tokenUtil.GenerateJwtToken(ref_id);
                        token.token = accessToken;
                        return token;
                    }
                    else
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_CREDENTIALS);
                    }
                }

            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }

        /// <summary>
        /// Method for Forget OTP Verification
        /// </summary>
        /// <param name="ref_id">Reference Id</param>
        /// <param name="otp"> User Inputted OTP</param>
        /// <returns>token</returns>
        public Token forgetverification(string ref_id, string otp)
        {
            try
            {
                if (ref_id == null || otp == null)
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.NULL_PARAMETER);
                }
                else
                {
                    Token token = new Token();
                    OtpDA otpDA = new OtpDA(configuration);
                    bool user = otpDA.Verifyotp(ref_id, otp, Parameter.Forget);
                    if (user)
                    {
                        string accessToken = tokenUtil.GenerateJwtToken(ref_id);
                        token.token = accessToken;
                        return token;
                    }
                    else
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_CREDENTIALS);
                    }
                }

            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }

       






        //Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        readonly string accountSid = OTPCredentials.GetAccoundSID();
        //Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
        readonly string authToken = OTPCredentials.GetAuthToken();
        readonly string TwilioFreePhoneNumber = OTPCredentials.GetTwilioFreePhoneNumber();
        /// <summary>
        /// function to generate OTP
        /// </summary>
        /// <param name="mobileNumber"> Mobile Number</param>
        /// <returns> OTP which is generated for the input mobile number</returns>
        public string? MobileOtp(string mobileNumber, string otp)
        {
            try
            {


                TwilioClient.Init(accountSid, authToken);
                var message = MessageResource.Create(
                    body: otp,
                    from: new Twilio.Types.PhoneNumber(TwilioFreePhoneNumber),
                    to: new Twilio.Types.PhoneNumber("+91" + mobileNumber)

                );

                Console.WriteLine(message.Sid);
                return message.Body.Substring(38);

            }
            catch
            {
                return "Server Error";
            }

        }
    }
}
