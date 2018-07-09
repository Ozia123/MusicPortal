using Microsoft.Extensions.Configuration;

namespace MusicPortal.DAL.Helpers {
    public class ConfigurationHelper {
        private readonly IConfiguration configuration;

        public IConfiguration Configuration => configuration;

        public ConfigurationHelper() {
            configuration = BuildConfiguration();
        }

        private IConfiguration BuildConfiguration() {
            return new ConfigurationBuilder()
                        .AddJsonFile("~/appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
        }
    }
}
