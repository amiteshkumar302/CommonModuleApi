using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util;
using MindITCommonModulesAPI.Util.StoredProcedures;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;

namespace MindITCommonModulesAPI.DataAccess
{
    public class UserData
    {
        private readonly SendOTPVerification sendotp;
        private readonly VerifyOTPVerification verifyOtp;
        // call DBHelper class
        private readonly DBHelper dbHelper;

        public UserData(IConfiguration configuration)
        {
            dbHelper = new DBHelper(configuration);
            sendotp = new SendOTPVerification(configuration);
            verifyOtp = new VerifyOTPVerification(configuration);
        }

        // This method fetches all non-deleted users list from dataabase
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> userList = new List<UserModel>();
            try
            {

                //get connection with database to get the user details
                MySqlCommand cmd = new MySqlCommand(ProcedureName.getUsers, dbHelper.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                //open connection
                if (dbHelper.OpenConnection())
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //Read all user details 
                        UserModel user = new UserModel();

                        user.userId = rdr[Column.userId].ToString() ?? "";
                        user.firstName = rdr[Column.firstName].ToString() ?? "";
                        user.Email = rdr[Column.Email].ToString() ?? "";
                        user.MobileNo = rdr[Column.MobileNo].ToString() ?? "";
                        user.lastName = rdr[Column.lastName].ToString() ?? "";
                        user.isDeleted = Convert.ToInt32(rdr[Column.isDeleted].ToString()) as int? ?? Convert.ToInt32("");
                        user.isActive = Convert.ToInt32(rdr[Column.isActive].ToString()) as int? ?? Convert.ToInt32("");
                        userList.Add(user);

                    }

                }
                // IF list of non-deleted users details is empty return exception  "NO_CONTENT"
                if (userList.Count == 0)
                {

                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.NO_CONTENT);
                }

            }
            catch (CustomAPIExcpetion)
            {

                throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);

            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return userList;
        }
        // This method  User Details as per permission 
        public UserModel Updateuser(UserModel updateUser)
        {

            try
            {
                //get connection with database to update the user details
                MySqlCommand cmd = new MySqlCommand(ProcedureName.updateUsers, dbHelper.GetConnection());

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(Parameter._roles, JsonConvert.SerializeObject(updateUser.RolesList));
                cmd.Parameters.AddWithValue(Parameter._userId, updateUser.userId);
                cmd.Parameters.AddWithValue(Parameter._firstName, updateUser.firstName);
                cmd.Parameters.AddWithValue(Parameter._lastName, updateUser.lastName);

                // open connection 
                dbHelper.OpenConnection();



                // Check if user firstName and lastName is empty then return exception
                if (updateUser.firstName != null && updateUser.lastName != null)
                {
                    cmd.ExecuteNonQuery();
                }

                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_Name);
                }

            }

            catch (CustomAPIExcpetion)
            {
                if (updateUser.firstName == null || updateUser.lastName == null)
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_Name);
                }

                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);
                }
            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return updateUser;
        }


        // call OTP class method Sendotp to send the OTP
        public void Send(Useremail useremail)
        {
            try
            {
                sendotp.Sendotp(useremail);
            }
            catch (CustomAPIExcpetion customAPIExcpetion)
            {
                throw customAPIExcpetion;
            }
        }
        // call OTP class and call method "Verifyotp" 
        public void verifiy(UserVerify userVerify)
        {
            try
            {
                verifyOtp.Verifyotp(userVerify);

            }
            catch (CustomAPIExcpetion customAPIExcpetion)
            {
                throw customAPIExcpetion;
            }
        }
    }
}
