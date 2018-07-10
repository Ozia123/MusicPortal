using AutoMapper;
using IF.Lastfm.Core.Objects;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;
using System;
using System.Linq;

namespace MusicPortal.ViewModels.MappingProfiles {
    public class AlbumProfile : Profile {
        private static readonly string defaultImageUrl = "http://www.back2gaming.com/wp-content/gallery/tt-esports-shockspin/white_label.gif";

        public AlbumProfile() {
            CreateMap<Album, AlbumViewModel>()
               .ReverseMap();

            CreateMap<LastAlbum, AlbumViewModel>()
                .ForMember(x => x.PictureURL, y => y.MapFrom(z => string.IsNullOrEmpty(z.Images.Large.AbsoluteUri) ? defaultImageUrl : z.Images.Large.AbsoluteUri))
                .ForMember(x => x.PlayCount, y => y.MapFrom(z => z.PlayCount ?? 0))
                .ForMember(x => x.ReleaseDate, y => y.MapFrom(z => z.ReleaseDateUtc ?? DateTime.UtcNow))
                .ForMember(x => x.ArtistName, y => y.MapFrom(z => z.ArtistName))
                .ForMember(x => x.TrackNames, y => y.MapFrom(z => z.Tracks.Select(t => t.Name).ToList()));
        } 
    }
}
