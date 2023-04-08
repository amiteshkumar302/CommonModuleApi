using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util.StoredProcedures;
using MySql.Data.MySqlClient;
using System.Data;

namespace MindITCommonModulesAPI.Util
{
    public class VerifyOTPVerification
    {
        private readonly DBHelper dbHelper;


        private readonly IConfiguration configuration;
        public VerifyOTPVerification(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbHelper = new DBHelper(configuration);

        }
        // verify the email address through OTP verification
        public bool Verifyotp(UserVerify userVerify)
        {


            UserVerify user = new UserVerify();
            try
            {
                // Get connection with database 
                MySqlCommand cmd = new MySqlCommand(ProcedureName.verifyOtp, dbHelper.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(Parameter._OTP, userVerify.OTP);
                cmd.Parameters.AddWithValue(Parameter._ReferenceID, userVerify.ReferenceID);

                if (dbHelper.OpenConnection())
                {
                    // Read OTP from database
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {


                        user.OTP = rdr[Column.OTP].ToString() ?? "";
                        user.ReferenceID = rdr[Column.ReferenceID].ToString()?? "";
                    }
                }
                // check if OTP and ReferenceID is entered wromg then return exception "Enter valid OTP and ReferenceID "
                if (userVerify.OTP.Equals(user.OTP) && userVerify.ReferenceID.Equals(user.ReferenceID))
                {
                    return true;

                }
                else
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_OTP_ReferenceID);

                }


            }
            catch (CustomAPIExcpetion)
            {
                // if Entered OTP and ReferenceID is not match with valid OTP and ReferenceID then return exception
                if (userVerify.OTP != user.OTP && userVerify.ReferenceID != user.ReferenceID)
                {
                    throw new CustomAPIExcpetion(CustomErrorCodeEnum.INVALID_OTP_ReferenceID);
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
        }

    }
}
