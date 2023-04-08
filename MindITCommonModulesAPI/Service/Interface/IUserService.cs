using MindITCommonModulesAPI.Model;

namespace MindITCommonModulesAPI.Service.Interface
{
    public interface IUserService
    {
        ReferenceId ForgotPassword(ForgotPassword forgotUsername);
    }
}
