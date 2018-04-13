using IF.Lastfm.Core.Api.Helpers;
using IF.Lastfm.Core.Objects;
using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPortal.BLL.BusinessModels {
    // TODO: Можно использовать AutoMapper, будет проще
    public class LastFmDtoMapper {
        private static string defaultImageURL = @"http://www.back2gaming.com/wp-content/gallery/tt-esports-shockspin/white_label.gif";

        public ArtistDto MapArtist(LastArtist artist) {
            return new ArtistDto {
                Name = artist.Name,
                Biography = "no bio",
                PictureURL = artist.MainImage.Mega.AbsoluteUri
            };
        }

        public AlbumDto MapAlbum(LastAlbum album) {
            return new AlbumDto {
                Name = album.Name,
                PictureURL = album.Images.Large.AbsoluteUri ?? defaultImageURL,
                PlayCount = album.PlayCount ?? 0,
                ReleaseDate = album.ReleaseDateUtc ?? DateTime.Now,
                ArtistName = album.ArtistName,
                TrackNames = album.Tracks.Select(t => t.Name).ToList()
            };
        }

        public string GetTrackImage(LastImageSet images) {
            if (images == null) {
                return defaultImageURL;
            }
            return images.Large.AbsoluteUri;
        }

        public TrackDto MapTrack(LastTrack track) {
            return new TrackDto {
                Name = track.Name,
                Rank = track.Rank ?? 0,
                PlayCount = track.PlayCount ?? 0,
                PictureURL = GetTrackImage(track.Images),
                TrackURL = track.Url.AbsoluteUri,
                ArtistName = track.ArtistName ?? "",
                AlbumName = track.AlbumName ?? ""
            };
        }

        public List<ArtistDto> MapArtists(PageResponse<LastArtist> artists) {
            List<ArtistDto> artistDtoList = new List<ArtistDto>();
            foreach (var artist in artists) {
                ArtistDto artistDto = MapArtist(artist);
                artistDtoList.Add(artistDto);
            }
            return artistDtoList;
        }

        public List<AlbumDto> MapAlbums(PageResponse<LastAlbum> albums) {
            List<AlbumDto> albumDtoList = new List<AlbumDto>();
            foreach (var album in albums) {
                if (album.Name.Equals("(null)")) {
                    break;
                }
                AlbumDto albumDto = MapAlbum(album);
                albumDtoList.Add(albumDto);
            }
            return albumDtoList;
        }

        public List<TrackDto> MapTracks(IEnumerable<LastTrack> tracks) {
            List<TrackDto> trackDtoList = new List<TrackDto>();
            foreach (var track in tracks) {
                if (track.Name.Equals("(null)")) {
                    break;
                }
                TrackDto trackDto = MapTrack(track);
                trackDtoList.Add(trackDto);
            }
            return trackDtoList;
        }
    }
}
