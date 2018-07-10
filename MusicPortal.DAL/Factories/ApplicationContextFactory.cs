using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;

namespace MusicPortal.DAL.Factories {
    public static class ApplicationContextFactory {
        public static ApplicationContext GetContext(string connectionString) {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("MusicPortal.DAL"))
                .Options;

            return new ApplicationContext(dbOptions);
        }
    }
}
