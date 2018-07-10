using AutoMapper;
using Microsoft.Extensions.Configuration;
using MusicPortal.Facade.Facades;
using MusicPortal.Facade.Interfaces;

namespace MusicPortal.BLL.UnitTests.Factories {
    public static class FacadeFactory {
        private static readonly IMapper mapper;
        private static readonly IConfiguration configuration;

        static FacadeFactory() {
            mapper = MapperFactory.GetMapper();
            configuration = ConfigurationFactory.BuildConfiguration();
        }

        public static IMusicPortalClient GetMusicPortalClient() {
            return new MusicPortalClient(mapper, configuration);
        }
    }
}
