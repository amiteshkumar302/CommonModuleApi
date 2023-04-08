using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util;
using MindITCommonModulesAPI.Util.StoredProcedures;
using MySql.Data.MySqlClient;
using System.Data;

namespace MindITCommonModulesAPI.DataAccess
{
    public class LoginDA
    {
        readonly DBHelper dbHelper;
        public LoginDA(IConfiguration configuration)
        {
            dbHelper = new DBHelper(configuration);
        }


        /// <summary>
        /// Verifying user authenticatiuon from db
        /// </summary>
        /// <param name="credentials">Username and Password</param>
        /// <returns>User Model class object</returns>
        /// <exception cref="CustomAPIExcpetion"></exception>

        public Login? VerifyUser(LoginCredentials? credentials)
        {

            Login? user = null;
            UserNameTypeUtils uservalidate = new UserNameTypeUtils();

            // Getting the Type of Username Entered by user
            string type = uservalidate.InputUserName(credentials.UserName);
            try
            {

                if (dbHelper.OpenConnection())
                {
                    //Establishing Connection and Declaring Stored Procedure
                    MySqlCommand cmd = new MySqlCommand(ProcedureName.VerifyLoginProcedureName, dbHelper.GetConnection())
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    //Passing Parameters
                    cmd.Parameters.AddWithValue(ProcedureName.UserName, credentials.UserName);
                    cmd.Parameters.AddWithValue(ProcedureName.UserPass, credentials.Password);
                    cmd.Parameters.AddWithValue(ProcedureName.LoginType, type.ToString());

                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        user = new Login();
                        user.UserId = (string)dataReader[ProcedureName.userId];
                        user.MobileNumber = (string)dataReader[ProcedureName.mobilenumber];
                        user.Email = (string)dataReader[ProcedureName.email];
                        user.Type = type;
                        user.OTPType = ProcedureName.Login;

                    }
                }

            }
            catch (Exception)
            {
                // Throwing Exception 
                throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);

            }
            finally
            {
                dbHelper.CloseConnection();
            }

            return user;

        }

        /// <summary>
        /// Resetting Password
        /// </summary>
        /// <param name="UserId">User Id from</param>
        /// <param name="Password">New Password</param>
        /// <returns>bool</returns>
        /// <exception cref="CustomAPIExcpetion"></exception>
        public bool ResetPassword(string UserId ,string Password)
        {
            try
            {

                if (dbHelper.OpenConnection())
                {
                    //Establishing Connection and Declaring Stored Procedure
                    MySqlCommand cmd = new MySqlCommand(ProcedureName.ResetPassProcedureName, dbHelper.GetConnection())
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    //Passing Parameters
                    cmd.Parameters.AddWithValue( ProcedureName.Password, Password);
                    cmd.Parameters.AddWithValue( ProcedureName.UserId, UserId);
                    MySqlParameter param_msg = new MySqlParameter(ProcedureName.Message, MySqlDbType.VarChar); // declare variable to show the message come from stored procedures
                    param_msg.Direction = ParameterDirection.Output; // Declaration Type
                    cmd.Parameters.Add(param_msg);

                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return true;

                    }
                }

            }
            catch (Exception)
            {
                // Throwing Exception 
                throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);

            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return false; 
        }

    }
}
