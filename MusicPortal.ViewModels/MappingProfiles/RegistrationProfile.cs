using AutoMapper;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.ViewModels.MappingProfiles {
    public class RegistrationProfile : Profile {
        public RegistrationProfile() {
            CreateMap<RegistrationViewModel, AppUser>()
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.Email));
        }
    }
}
