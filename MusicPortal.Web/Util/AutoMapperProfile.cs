using AutoMapper;
using MusicPortal.DAL.Entities;
using MusicPortal.Web.Models;
using MusicPortal.BLL.DTO;

namespace MusicPortal.Web.Util {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMapsForArtist();
            CreateMapsForAlbum();
            CreateMapsForTrack();
        }

        private void CreateMapsForArtist() {
            CreateMap<Artist, ArtistDto>()
               .ReverseMap();

            CreateMap<ArtistDto, Artist>()
                .ReverseMap();

            CreateMap<ArtistModel, ArtistDto>()
                .ReverseMap();

            CreateMap<ArtistDto, ArtistModel>()
                .ReverseMap();
        }

        private void CreateMapsForAlbum() {
            CreateMap<Album, AlbumDto>()
               .ReverseMap();

            CreateMap<AlbumDto, Album>()
                .ReverseMap();

            CreateMap<AlbumModel, AlbumDto>()
                .ReverseMap();

            CreateMap<AlbumDto, AlbumModel>()
                .ReverseMap();
        }

        private void CreateMapsForTrack() {
            CreateMap<Track, TrackDto>()
               .ReverseMap();

            CreateMap<TrackDto, Track>()
                .ReverseMap();

            CreateMap<TrackModel, TrackDto>()
                .ReverseMap();

            CreateMap<TrackDto, TrackModel>()
                .ReverseMap();
        }
    }
}