using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicPortal.DAL.EF {
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext> {
        public ApplicationContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MusicPortalDb;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("MusicPortal.DAL"));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}