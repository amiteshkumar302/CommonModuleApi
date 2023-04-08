namespace MindITCommonModulesAPI.Util
{
    public class TwoFactorySetup
    {
        public static string GetTwoFactory()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false)
                .Build();
            return configuration.GetSection("2Authentication").GetSection("2faEnable").Value;
        }


    }
}
