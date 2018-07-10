using AutoMapper;
using MusicPortal.ViewModels.MappingConfiguration;

namespace MusicPortal.BLL.UnitTests.Factories {
    public static class MapperFactory {
        public static IMapper GetMapper() {
            return MappingConfiguration.GetConfiguration().CreateMapper();
        }
    }
}
