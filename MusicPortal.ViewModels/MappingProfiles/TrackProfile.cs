using AutoMapper;
using IF.Lastfm.Core.Objects;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.ViewModels.MappingProfiles {
    public class TrackProfile : Profile {
        private static readonly string defaultImageUrl = "http://www.back2gaming.com/wp-content/gallery/tt-esports-shockspin/white_label.gif";

        public TrackProfile() {
            CreateMap<TrackViewModel, Track>()
                .ForMember(x => x.ArtistId, y => y.MapFrom(z => z.ArtistId));
            
            CreateMap<Track, TrackViewModel>()
                .ForMember(x => x.ArtistName, y => y.MapFrom(z => z.Artist.Name));

            CreateMap<LastTrack, TrackViewModel>()
                .ForMember(x => x.Rank, y => y.MapFrom(z => z.Rank ?? 0))
                .ForMember(x => x.PlayCount, y => y.MapFrom(z => z.PlayCount ?? 0))
                .ForMember(x => x.PictureURL, y => y.MapFrom(z => z.Images == null ? defaultImageUrl : z.Images.Large.AbsoluteUri))
                .ForMember(x => x.TrackURL, y => y.MapFrom(z => z.Url.AbsoluteUri))
                .ForMember(x => x.ArtistName, y => y.MapFrom(z => string.IsNullOrEmpty(z.ArtistName) ? "unknown" : z.ArtistName))
                .ForMember(x => x.AlbumName, y => y.MapFrom(z => z.AlbumName ?? string.Empty));
        }
    }
}
