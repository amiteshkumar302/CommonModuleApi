using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Service.Interface;

namespace MindITCommonModulesAPI.Service
{
    public class LoginService:ILoginService
    {
        private readonly IConfiguration configuration;

        public LoginService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool ResetPassword(string UserId, string Password)
        {
            try
            {
                if (UserId == null || Password == null)
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.NULL_PARAMETER);
                }
                else
                {
                    LoginDA loginDA = new LoginDA(configuration);
                    bool result = loginDA.ResetPassword(UserId, Password);
                    return result;
                }

            }
            catch (CustomAPIExcpetion customApiException)
            {
                throw customApiException;
            }
        }

    }
}
