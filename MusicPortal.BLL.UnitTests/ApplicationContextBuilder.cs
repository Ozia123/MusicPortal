using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;

namespace MusicPortal.BLL.UnitTests {
    class ApplicationContextBuilder {
        private static readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=MusicPortalDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly ApplicationContext _context;
        
        public ApplicationContextBuilder() {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("MusicPortal.DAL"))
                .Options;
            _context = new ApplicationContext(dbOptions);
        }

        public ApplicationContext GetApplicationContext() {
            return _context;
        }
    }
}
