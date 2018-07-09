using MusicPortal.DAL.EF;
using MusicPortal.DAL.Factories;
using System.Configuration;

namespace MusicPortal.WebForms.Factories {
    public static class ContextFactory {
        private static readonly string connectionString;

        static ContextFactory() {
            connectionString = ConfigurationManager.ConnectionStrings["ApplicationContext"].ConnectionString;
        }

        public static ApplicationContext GetContext() {
            return ApplicationContextFactory.GetContext(connectionString);
        }
    }
}