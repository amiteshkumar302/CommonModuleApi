
using MindITCommonModulesAPI.Service.Interface;
using MindITCommonModulesAPI.Util.TwilioSetup;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MindITCommonModulesAPI.Util
{
    public class OtpService 
    {
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
        public string? MobileOtp(string mobileNumber , string otp)
        {
            try
            {
                
            
                TwilioClient.Init(accountSid, authToken);
                var message = MessageResource.Create(
                    body: otp,
                    from: new Twilio.Types.PhoneNumber(TwilioFreePhoneNumber),
                    to: new Twilio.Types.PhoneNumber("+91"+mobileNumber)
                    
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
