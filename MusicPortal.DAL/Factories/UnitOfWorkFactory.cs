using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Repositories;

namespace MusicPortal.DAL.Factories {
    public static class UnitOfWorkFactory {
        public static IUnitOfWork GetUnitOfWork(string connectionString) {
            var context = ApplicationContextFactory.GetContext(connectionString);
            return new UnitOfWork(context);
        }

        public static IUnitOfWork GetUnitOfWork(ApplicationContext context) {
            return new UnitOfWork(context);
        }
    }
}
