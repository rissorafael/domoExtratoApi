using Microsoft.Extensions.Configuration;


namespace DomoExtrato.Infra.CrossCutting.ExtensionMethods
{
    public static class ExatractConfiguration
    {
        static IConfiguration Config;

        public static void Initialize(IConfiguration configuration)
        {
            Config = configuration;
        }

        public static string GetConnectionString
        {
            get
            {
                return GetConnection();
            }
        }

        private static string GetConnection()
        {
            var connectionString = Config.GetConnectionString("SqlServerConnection");
            return connectionString;
        }
    }
}
