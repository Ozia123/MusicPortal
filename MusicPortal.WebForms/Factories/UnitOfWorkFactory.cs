using System.Configuration;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.WebForms.Factories {
    public static class UnitOfWorkFactory {
        private static readonly string connectionString;

        static UnitOfWorkFactory() {
            connectionString = ConfigurationManager.ConnectionStrings["ApplicationContext"].ConnectionString;
        }

        public static IUnitOfWork GetUnitOfWork() {
            return DAL.Factories.UnitOfWorkFactory.GetUnitOfWork(connectionString);
        }
    }
}