using IF.Lastfm.Core.Api;
using IF.Lastfm.Core.Objects;
using MusicPortal.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.BLL.BusinessModels {
    public class LastFm {
        private readonly LastfmClient client;
        private readonly LastFmDtoMapper lastFmDtoMapper;

        private readonly string apiKey = "cae6262226682d177ef0ac9ade016c26";
        private readonly string apiSecret = "0a5fd9cdf508889ed63ec727c3d6a078";

        public LastFm() {
            client = new LastfmClient(apiKey, apiSecret);
            lastFmDtoMapper = new LastFmDtoMapper();
        }

        private async Task<LastArtist> GetFullInfoArtistFromLastFm(string artistName) {
            LastArtist artist = (await client.Artist.GetInfoAsync(artistName)).Content ?? new LastArtist();
            artist.Bio = artist.Bio ?? new LastWiki();
            if (artist.Bio.Content == null) {
                artist.Bio.Content = "no bio";
            }
            return artist;
        }

        public async Task<ArtistDto> GetFullInfoArtist(string artistName) {
            var fullInfoArtist = await GetFullInfoArtistFromLastFm(artistName);
            return lastFmDtoMapper.MapArtist(fullInfoArtist);
        }

        public async Task<AlbumDto> GetFullInfoAlbum(string artistName, string albumName) {
            var fullInfoAlbum = await client.Album.GetInfoAsync(artistName, albumName, false);
            return lastFmDtoMapper.MapAlbum(fullInfoAlbum.Content);
        }

        public async Task<TrackDto> GetFullInfoTrack(string artistName, string trackName) {
            var fullInfoTrack = (await client.Track.GetInfoAsync(trackName, artistName)).Content;
            return lastFmDtoMapper.MapTrack(fullInfoTrack);
        }

        public async Task<List<ArtistDto>> GetFullInfoArtists(List<ArtistDto> artists) {
            for (int i = 0; i < artists.Count; i++) {
                artists[i] = await GetFullInfoArtist(artists[i].Name);
            }
            return artists;
        }

        public async Task<List<ArtistDto>> GetSimilarArtists(string name) {
            var artists = await client.Artist.GetSimilarAsync(name);
            return lastFmDtoMapper.MapArtists(artists);
        }

        public async Task<List<ArtistDto>> GetTopArtists(int page = 1, int itemsPerPage = 20) {
            var artists = await client.Chart.GetTopArtistsAsync(page, itemsPerPage);
            return lastFmDtoMapper.MapArtists(artists);
        }

        public async Task<List<TrackDto>> GetTopTracks(int page = 1, int itemsPerPage = 20) {
            var tracks = await client.Chart.GetTopTracksAsync(page, itemsPerPage);
            return lastFmDtoMapper.MapTracks(tracks);
        }

        public async Task<List<AlbumDto>> GetTopArtistsAlbums(string name, int page = 1, int itemsPerPage = 20) {
            var albums = await client.Artist.GetTopAlbumsAsync(name, false, page, itemsPerPage);
            return lastFmDtoMapper.MapAlbums(albums);
        }

        public async Task<List<TrackDto>> GetTopArtistsTracks(string name, int page = 1, int itemsPerPage = 20) {
            var tracks = await client.Artist.GetTopTracksAsync(name, false, page, itemsPerPage);
            return lastFmDtoMapper.MapTracks(tracks);
        }
    }
}