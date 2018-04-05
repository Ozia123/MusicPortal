using IF.Lastfm.Core.Api.Helpers;
using IF.Lastfm.Core.Objects;
using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;

namespace MusicPortal.BLL.BusinessModels {
    public static class LastFmDtoMapper {

        public static ArtistDto MapArtist(LastArtist artist) {
            return new ArtistDto {
                Name = artist.Name,
                Biography = "no bio",
                PictureURL = artist.MainImage.Mega.AbsoluteUri
            };
        }

        public static AlbumDto MapAlbum(LastAlbum album) {
            return new AlbumDto {
                Name = album.Name,
                PictureURL = album.Images.Mega.AbsoluteUri,
                PlayCount = album.PlayCount ?? 0,
                ReleaseDate = album.ReleaseDateUtc ?? DateTime.Now,
            };
        }

        public static TrackDto MapTrack(LastTrack track) {
            return new TrackDto {
                Name = track.Name,
                Rank = track.Rank ?? 0,
                PlayCount = track.PlayCount ?? 0,
                PictureURL = track.Images.Large.AbsoluteUri,
                TrackURL = track.Url.AbsoluteUri,
            };
        }

        public static List<ArtistDto> MapArtists(PageResponse<LastArtist> artists) {
            List<ArtistDto> artistDtoList = new List<ArtistDto>();
            foreach (var artist in artists) {
                ArtistDto artistDto = MapArtist(artist);
                artistDtoList.Add(artistDto);
            }
            return artistDtoList;
        }

        public static List<AlbumDto> MapAlbums(PageResponse<LastAlbum> albums) {
            List<AlbumDto> albumDtoList = new List<AlbumDto>();
            foreach (var album in albums) {
                AlbumDto albumDto = MapAlbum(album);
                albumDtoList.Add(albumDto);
            }
            return albumDtoList;
        }

        public static List<TrackDto> MapTracks(PageResponse<LastTrack> tracks) {
            List<TrackDto> trackDtoList = new List<TrackDto>();
            foreach (var track in tracks) {
                TrackDto trackDto = MapTrack(track);
                trackDtoList.Add(trackDto);
            }
            return trackDtoList;
        }
    }
}
