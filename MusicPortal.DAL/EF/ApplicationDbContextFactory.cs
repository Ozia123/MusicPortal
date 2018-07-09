using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MusicPortal.DAL.EF {
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext> {
        private IConfiguration _configuration;

        public ApplicationDbContextFactory(IConfiguration configuration) : base() {
            _configuration = configuration;
        }

        public ApplicationContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ApplicationContext"), b => b.MigrationsAssembly("MusicPortal.DAL"));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}