namespace MindITCommonModulesAPI.Service.Interface
{
    public interface ILoginService
    {
        public bool ResetPassword(string UserId, string Password);
    }
}
