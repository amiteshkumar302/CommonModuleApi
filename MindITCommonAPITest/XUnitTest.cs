using Microsoft.Extensions.Configuration;
using MindITCommonModulesAPI.DataAccess;
using MindITCommonModulesAPI.Model;
using MindITCommonModulesAPI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindITCommonAPITest
{
    [TestClass]
    public class XUnitTest
    {
        private readonly VerifyOTPVerification? userVerifyOTP;
        // private readonly UserData? userData;
        private readonly IConfiguration? configuration;
        //private UserModel user;

        public XUnitTest()
        {


            configuration = new ConfigurationBuilder()
                  .AddInMemoryCollection()
                  .Build();
            userVerifyOTP = new VerifyOTPVerification(configuration);
            //userData = new UserData(configuration);

        }
    }
}
        /*
                [TestMethod]
                public void Test1()
                {

                    UserModel result = userData.Update(user);
                    Assert.IsNotNull(result);
                }
        
                [TestMethod]
                public void Test2()
                {

                    List<UserModel> result = userData.GetAll();
                    Assert.IsNotNull(result);
                }*/


      
     
    

