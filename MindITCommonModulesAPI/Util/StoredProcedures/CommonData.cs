namespace MindITCommonModulesAPI.Util.StoredProcedures
{
    
    public static class SPMessage
    {
        public static readonly string MSG = "_msg";
        public static readonly string Message = "Message";
    }

    public static class Parameter
    {
        public static readonly string RoleID = "_roleID";
        public static readonly string RoleType = "_roleType";
        public static readonly string IsActive = "_activeStatus";
        public static readonly string DeleteStatus = "_deleteStatus";
        public static readonly string CreatedOn = "CreatedOn";
        public static readonly string UpdatedOn = "UpdatedOn";
        public static readonly string Login = "Login";
        public static readonly string Forget = "Forget";


        public static readonly string _roles = "@_roles";
        public static readonly string _userId = "@_userId";
        public static readonly string _firstName = "@_firstName";
        public static readonly string _lastName = "@_lastName";
        public static readonly string _createdBy = "@_createdBy";
        public static readonly string _OTP = "_OTP";
        public static readonly string _ReferenceID = "_ReferenceID";
        public static readonly string _EmailId = "_EmailId";

    }

    public static class Column
    {
        public static readonly string RoleID = "RoleID";
        public static readonly string RoleType = "RoleType";
        public static readonly string Status = "IsActive";
        public static readonly string DeleteStatus = "IsDeleted";
        public static readonly string CreatedOn = "CreatedOn";
        public static readonly string UpdatedOn = "UpdatedOn";


        public static readonly string userId = "userId";
        public static readonly string firstName = "firstName";
        public static readonly string Email = "Email";
        public static readonly string MobileNo = "MobileNo";
        public static readonly string lastName = "lastName";
        public static readonly string isDeleted = "isDeleted";
        public static readonly string isActive = "isActive";
        public static readonly string OTP = "OTP";
        public static readonly string ReferenceID = "ReferenceID";

    }



    public static class ProcedureName
    {
        public static readonly string SPGetRoles = "Sp_Get_Roles";
        public static readonly string SPCreateRole = "Sp_Create_Role";
        public static readonly string SPUpdateRole = "Sp_Update_RoleType";
        public static readonly string SPActiveStatus = "Sp_Update_Active_Status_Role";
        public static readonly string SPGetRole = "Sp_Get_Role";
        public static readonly string SPGetRoleDetails = "Sp_Get_Role_By_Type";
        public static readonly string SPDeleteRole= "Sp_Update_Delete_Status_Role";
        public static readonly string VerifyLoginProcedureName = "Sp_verify_login";
        public static readonly string ForgotPassVerifyProcedureName = "Sp_Forgot_Pass_Verify";
        public static readonly string LoginOTPSaveProcedureName = "SP_Login_OTP_Save";
        public static readonly string ForgetOTPSaveProcedureName = "SP_Forget_OTP_Save";
        public static readonly string ResetPassProcedureName = "SP_reset_Password";
        public static readonly string UserName = "@_User_name";
        public static readonly string UserPass = "@_User_pass";
        public static readonly string LoginType = "@_Login_Type";

        public static readonly string UserId = "@_User_Id";
        public static readonly string OTP = "@_OTP";
        public static readonly string Message = "_msg";
        public static readonly string Login = "Login";
        public static readonly string Forget = "Forget";
        public static readonly string OTPType = "@_OTP_Type";

        public static readonly string? ReferenceID = "_ReferenceID";
        public static readonly string userId = "User_Id";
        public static readonly string mobilenumber = "mobileNo";
        public static readonly string email = "email";
        public static readonly string userIdForget = "userId";
        public static readonly string Referenceid = "@_ref_id";
         public static readonly string OTPverification = "sp_otpverification";
        public static readonly string Password = "@_password";



        public static readonly string getUsers = "GetData_procedure";
        public static readonly string updateUsers = "Update_procedure";
        public static readonly string sendOtp = "Save_OTP_procedure";
        public static readonly string verifyOtp = "OTPVer_procedure";

    }

}
