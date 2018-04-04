using AutoMapper;

namespace MusicPortal.Web.Util {
    public class AutoMapperConfiguration {
        public static void Configure() {
            Mapper.Initialize(cfg => {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }
}