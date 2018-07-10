using Microsoft.Extensions.Configuration;

namespace MusicPortal.DAL.Factories {
    public static class ConfigurationFactory {
        public static IConfiguration BuildConfiguration() {
            return new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
