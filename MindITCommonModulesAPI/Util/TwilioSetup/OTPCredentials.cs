namespace MindITCommonModulesAPI.Util.TwilioSetup
{
    public class OTPCredentials
    {
        public static string GetAccoundSID()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false)
                .Build();
            return configuration.GetSection("Twilio").GetSection("accountSid").Value;
        }
        public static string GetAuthToken()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false)
                .Build();
            return configuration.GetSection("Twilio").GetSection("AuthToken").Value;
        }
        public static string GetTwilioFreePhoneNumber()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false)
                .Build();
            return configuration.GetSection("Twilio").GetSection("TwilioFreePhoneNumber").Value;
        }
    }
}
