using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Helpers;

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
