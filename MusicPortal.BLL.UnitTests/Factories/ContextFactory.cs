using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPortal.DAL.EF;

namespace MusicPortal.BLL.UnitTests.Factories {
    public static class ContextFactory {
        private static readonly IConfiguration configuration;

        static ContextFactory() {
            configuration = ConfigurationFactory.BuildConfiguration();
        }

        public static ApplicationContext GetContext() {
            var connectionString = configuration.GetConnectionString("TestsContext");

            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("MusicPortal.DAL"))
                .Options;

            return new ApplicationContext(dbOptions);
        }
    }
}
