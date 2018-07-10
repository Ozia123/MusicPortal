using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MusicPortal.DAL.Factories;

namespace MusicPortal.DAL.EF {
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext> {
        private IConfiguration configuration;

        public ApplicationDbContextFactory() : base() {
            configuration = ConfigurationFactory.BuildConfiguration();
        }

        public ApplicationDbContextFactory(IConfiguration configuration) : base() {
            this.configuration = configuration;
        }

        public ApplicationContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApplicationContext"), b => b.MigrationsAssembly("MusicPortal.DAL"));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}