using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util;
using MindITCommonModulesAPI.Util.StoredProcedures;
using MySql.Data.MySqlClient;
using System.Data;

namespace MindITCommonModulesAPI.DataAccess
{
    public class OtpDA

    {

        readonly DBHelper  dbHelper;

       
        public OtpDA(IConfiguration configuration)
        {
            dbHelper = new DBHelper(configuration);
        }

        /// <summary>
        /// Saving OTP in otpTable
        /// </summary>
        /// <param name="otps"> User Model Class object</param>
        /// <returns>Boolean Value whether OTP is stored successfully</returns>
        /// <exception cref="CustomAPIExcpetion"></exception>
        public string SaveOTP(Login otps)
        {
            string referenceId = null;
            try
            {
                ReferenceIDGenerate refId = new ReferenceIDGenerate();

                if (dbHelper.OpenConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(ProcedureName.LoginOTPSaveProcedureName, dbHelper.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;

                     referenceId = refId.GenerateRefId();
                    //Passing Parameters
                    cmd.Parameters.AddWithValue(ProcedureName.UserId, otps.UserId);
                    cmd.Parameters.AddWithValue(ProcedureName.ReferenceID, referenceId);
                    cmd.Parameters.AddWithValue(ProcedureName.OTP, otps.OTP);
                    cmd.Parameters.AddWithValue(ProcedureName.OTPType, otps.OTPType);

                    MySqlParameter param_msg = new MySqlParameter(ProcedureName.Message, MySqlDbType.VarChar); // declare variable to show the message come from stored procedures
                    param_msg.Direction = ParameterDirection.Output; // Declaration Type
                    cmd.Parameters.Add(param_msg);   // Add Parameters for out msg

                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.Read())
                    {
                        throw new CustomAPIExcpetion(CustomErrorCodeEnum.INTERNAL_SERVER_ERROR);
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
            return referenceId;
        }

       /// <summary>
       /// Verifying OTP from Database
       /// </summary>
       /// <param name="ref_id">Reference Id </param>
       /// <param name="otp">Inputed OTP </param>
       /// <param name="type">Type of OTP if it is Login or for Forget</param>
       /// <returns></returns>
       /// <exception cref="CustomAPIExcpetion"></exception>
        public bool Verifyotp(string ref_id, string otp, string type)
        {
            bool verify = false;


            try
            {
                //connection through stored procedure

                if (dbHelper.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(ProcedureName.OTPverification, dbHelper.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Taking input
                    cmd.Parameters.AddWithValue(ProcedureName.Referenceid, ref_id);
                    cmd.Parameters.AddWithValue(ProcedureName.OTP, otp);
                    cmd.Parameters.AddWithValue(ProcedureName.OTPType, type);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                     
                        verify = true;
                        return verify;
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
            return verify;

        }



    }
}
