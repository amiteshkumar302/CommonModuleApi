using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service.Interface;
using MindITCommonModulesAPI.Util;
using MindITCommonModulesAPI.Util.StoredProcedures;

namespace MindITCommonModulesAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserData userdata;
        private readonly TokenUtil tokenUtil;
        private readonly IConfiguration configuration;

        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
            tokenUtil = new TokenUtil(configuration);
            userdata = new UserData(configuration);

        }
        readonly string factory = TwoFactorySetup.GetTwoFactory(); 
        /// <summary>
        /// Authentication User while Login
        /// </summary>
        /// <param name="credentials"> Username and Password</param>
        /// <returns>User model Object</returns>
        public ReferenceId AuthenticateUser(LoginCredentials credentials)
        {
            string refid = null;
            ReferenceId RefId = new ReferenceId();
            try
            {
                if (credentials.Password == null || credentials.UserName == null)
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.NULL_PARAMETER);
                }
                else
                {
                    string? otpsend = null;
                    OtpDA otpDA = new OtpDA(configuration);
                    LoginDA loginDA = new LoginDA(configuration);
                    Login? user = loginDA.VerifyUser(credentials);
                    Random r = new Random();
                    string otp = r.Next(10001, 99999).ToString();
                    if (user != null)
                    {
                        if (user.Type == TYPEEnum.MobileNumber.ToString() && factory == "True")
                        {
                            IOTPService otpService = new OtpService(configuration);
                            otpsend = otpService.MobileOtp(user.MobileNumber, otp);
                            
                            
                        }
                        else if (user.Type == TYPEEnum.Email.ToString() && factory == "True")
                        {
                            // For Email OTP (Reference from Pradeep Code)


                        }
                        else if (user.Type == TYPEEnum.Username.ToString())
                        {
                            IOTPService otpService = new OtpService(configuration);
                            otpsend = otpService.MobileOtp(user.MobileNumber, otp);
                            // Email OTP
                        }
                        user.OTP = otp;
                        refid = otpDA.SaveOTP(user);
                        RefId.RefID = refid;
                        return RefId;
                    }
                    else
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_CREDENTIALS);
                }
            }
                
            catch(CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }
        /// <summary>
        /// call method GetAll fron UserData class and get the all user list
        /// </summary>
        /// <returns> Get all non-deleted users list success code 200</returns>
        public List<UserModel> GetUserList()
        {
            try
            {
                List<UserModel> userList = new List<UserModel>();
                // check if user list is not equal to  null return all user list 
                if (userList != null)
                {
                    userList = userdata.GetAllUsers();
                    return userList;
                }
                // else user list empty return exception 
                else
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.NO_CONTENT);
            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }
        /// <summary>
        /// update user details firstName and LastName 
        /// </summary>
        /// <param name="user">user is a Object of UserModel</param>
        /// <returns>User details Updated code success 200</returns>
        public UserModel UpdateUserDetails(UserModel user)
        {

            try
            {
                // if user is not null then update user details
                if (user != null)
                {
                    UserModel result = userdata.Updateuser(user);
                    return result;
                }
                // else user details are empty then return exception
                else
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_Name);
            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }
        // call UserData class Send method OTP
        public void Sendotp(Useremail useremail)
        {
            try
            {

                userdata.Send(useremail);

            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }
        // call UserData class  method "verifiy" to verify OTP
        public void OTPverified(UserVerify userVerify)
        {
            try
            {
                userdata.verifiy(userVerify);
            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }

    }
}
