using MindITCommonModulesAPI.Model;

namespace MindITCommonModulesAPI.Service.Interface
{
    public interface IOTPService
    {
        public string? MobileOtp(string mobileNumber,string otp);
        public Token Loginverification(string ref_id, string otp);
        public Token forgetverification(string ref_id, string otp);
    }
}
