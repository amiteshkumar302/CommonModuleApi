using MindITCommonModulesAPI.Model;


namespace MindITCommonModulesAPI.Service.Interface
{
    public interface IAuthService
    {
        
        public List<UserModel> GetUserList();

        public UserModel UpdateUserDetails(UserModel user);

        public void Sendotp(Useremail useremail);

        public void OTPverified(UserVerify userVerify);

        //Generating Reference Id after successfull Login Verification
        ReferenceId AuthenticateUser(LoginCredentials credentials);
        
    }
}
