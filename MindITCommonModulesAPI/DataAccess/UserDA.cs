using Microsoft.AspNetCore.Mvc;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util;
using MindITCommonModulesAPI.Util.StoredProcedures;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace MindITCommonModulesAPI.DataAccess
{
    public class UserDA
    {
        readonly DBHelper dbHelper;
        public UserDA(IConfiguration configuration)
        {
            dbHelper = new DBHelper(configuration);
        }
        

        //ForgotPassword API
        //Verifying username from DB
        /// <summary>
        /// Input is only Username
        /// </summary>
        /// <param name="forgotusername"></param>
        /// <returns>  User Model class object</returns>
        /// <exception cref="CustomAPIExcpetion"></exception>
        public Login? ForgotPassword(ForgotPassword? forgotUsername)
        {
            Login? ab = null;
            UserNameTypeUtils? userValidate = new();

            // Getting the Type of Username Entered by user
            string? type = userValidate.InputUserName(forgotUsername?.Username);
            try
            {
                
                if (dbHelper.OpenConnection())
                {
                    //Establishing Connection and Declaring Stored Procedure
                    MySqlCommand cmd = new(ProcedureName.ForgotPassVerifyProcedureName, dbHelper.GetConnection())
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    //Passing Parameters
                    cmd.Parameters.AddWithValue(ProcedureName.UserName, forgotUsername.Username);
                    cmd.Parameters.AddWithValue( ProcedureName.LoginType, type.ToString());

                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        ab = new Login();
                        ab.UserId = (string)dataReader[ProcedureName.userIdForget];
                        ab.Email = (string)dataReader[ProcedureName.email];
                        ab.MobileNumber = (string)dataReader[ProcedureName.mobilenumber];

                        //Generating OTP

                        ab.Type = type;
                        ab.OTPType = ProcedureName.Forget;
                    }

                }
            
            }
            catch (Exception ex)
            {
                throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);

            }
            finally
            {
                dbHelper.CloseConnection();
            }
            return ab;
        }


    }
    
    
}
