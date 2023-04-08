using Microsoft.Extensions.Configuration;
using MindITCommonModulesAPI.DataAccess;
//using MindITCommonModulesAPI.Model.Role;
using MindITCommonModulesAPI.Util;
using MindITCommonModulesAPI.Service;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Service.Interface;
using Moq;

namespace APIUnitTest
{
    [TestClass]
    public class TokenUtilTest
    {
        private readonly TokenUtil tokenUtil;
        
        private readonly IConfiguration configuration;
       
        //Arrange
        private readonly Dictionary<string,string> inMemorySettings = new Dictionary<string, string> {
           {"Jwt:Key", "SecretKey10125779374235322"},
            {"Jwt:Issuer","https://localhost" },
            {"Jwt:Audience","https://localhost" }
            ,{"ConnectionString","server = 103.234.187.254,3306; user = mindit_dev; password = mindit@123; database = mindit_common"}
    //...populate as needed for the test
    };
        public TokenUtilTest()
        {
            configuration = new ConfigurationBuilder()
                   .AddInMemoryCollection(inMemorySettings)
                   .Build();
            tokenUtil = new TokenUtil(configuration);
            DBHelper dBHelper=new DBHelper(configuration);


        }
        [TestMethod]
        public void TestMethod1()
        {
            string token = tokenUtil.GenerateJwtToken("1");
            Assert.IsNotNull(token);
        }

        //[TestMethod]
        //public void Authenticate()
        //{
        //    AuthService auth = new AuthService(configuration);
        //    string user = auth.AuthenticateUser();

        //}
        //Testing OTP Sending function
        //[TestMethod]
        //public void TestMethod3()
        //{

        //    MobileOTP mobileOTP = new MobileOTP();
        //    string? otp = mobileOTP.OTP("9518068917");
        //    Assert.AreNotEqual("Server Error", otp);
        //}


        //Test case for email validation
        [TestMethod]
        public void TestMethod4()
        {
            UserNameTypeUtils verifyUtils = new UserNameTypeUtils();
            string? verify = verifyUtils.InputUserName("abcd@example.com");
            Assert.AreEqual("Email",verify);
        }

        //Test case for mobile number validation
        [TestMethod]
        public void TestMethod5()
        {
            UserNameTypeUtils verifyUtils = new UserNameTypeUtils();
            string? verify = verifyUtils.InputUserName("9518068917");
            Assert.AreEqual( "MobileNumber", verify);
        }

        //Test case for username validation
        [TestMethod]
        public void TestMethod6()
        {
            UserNameTypeUtils verifyUtils = new UserNameTypeUtils();
            string? verify = verifyUtils.InputUserName("9518068");
            string? verify1 = verifyUtils.InputUserName("abc@12323");
            string? verify2 = verifyUtils.InputUserName("abc12323");

            Assert.AreEqual( "Username",verify,verify2,verify1);
        }

    }
}