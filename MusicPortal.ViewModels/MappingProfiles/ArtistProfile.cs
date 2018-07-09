using AutoMapper;
using IF.Lastfm.Core.Objects;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.ViewModels.MappingProfiles {
    public class ArtistProfile : Profile {
        public ArtistProfile() {
            CreateMap<Artist, ArtistViewModel>()
               .ReverseMap();

            CreateMap<LastArtist, ArtistViewModel>()
                .ForMember(x => x.Biography, y => y.MapFrom(z => z.Bio == null ? "no bio" : z.Bio.Content))
                .ForMember(x => x.PictureURL, y => y.MapFrom(z => z.MainImage.Mega.AbsoluteUri));
        }
    }
}
