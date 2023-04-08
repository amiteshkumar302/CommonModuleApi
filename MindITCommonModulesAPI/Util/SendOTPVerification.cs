using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.ExceptionHandler;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util.StoredProcedures;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace MindITCommonModulesAPI.Util
{
    public class SendOTPVerification
    {

        private readonly  ReferenceIDGenerate refId = new ReferenceIDGenerate();
        private readonly DBHelper dbHelper;


        private readonly IConfiguration configuration;
        public SendOTPVerification(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbHelper = new DBHelper(configuration);

        }
        // Sendotp method for send to otp on email of user 
        public void Sendotp(Useremail useremail)
        {
            string? to;
            // Generate RandomCode 
            string randomCode;
            string ReferenceID;

            String from, pass, messageBody;
            Random rand = new Random();
            randomCode = (rand.Next(99999)).ToString();
            ReferenceID = refId.GenerateRefId();
            MailMessage message = new MailMessage();
            to = useremail.EmailId;
            from = configuration["auth:from"];
            pass = configuration["auth:pass"];
            messageBody = "Your referenceid is = " + ReferenceID + "Your one time password = " + randomCode;
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Email Verification Code";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = Convert.ToInt32(configuration["Email:Port"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);

                MySqlCommand mysqlCmd = new MySqlCommand(ProcedureName.sendOtp, dbHelper.GetConnection());
                mysqlCmd.CommandType = CommandType.StoredProcedure;
                mysqlCmd.Parameters.AddWithValue(Parameter._OTP, randomCode);
                mysqlCmd.Parameters.AddWithValue(Parameter._ReferenceID, ReferenceID);

                // Open Connection
                if (dbHelper.OpenConnection())
                {
                    mysqlCmd.ExecuteNonQuery();
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
        }


    }
}
