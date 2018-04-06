using IF.Lastfm.Core.Api;
using IF.Lastfm.Core.Api.Helpers;
using IF.Lastfm.Core.Objects;
using MusicPortal.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPortal.BLL.BusinessModels {
    public class LastFm {
        private readonly LastfmClient client;

        private readonly string apiKey = "cae6262226682d177ef0ac9ade016c26";
        private readonly string apiSecret = "0a5fd9cdf508889ed63ec727c3d6a078";

        public LastFm() {
            client = new LastfmClient(apiKey, apiSecret);
        }

        private async Task<string> GetArtistBio(string name) {
            LastArtist artist = (await client.Artist.GetInfoAsync(name)).Content ?? new LastArtist();
            artist.Bio = artist.Bio ?? new LastWiki();
            return artist.Bio.Content ?? "no bio";
        }

        public async Task<ArtistDto> GetFullInfoArtist(ArtistDto artist) {
            artist.Biography = await GetArtistBio(artist.Name);
            return artist;
        }

        public async Task<List<ArtistDto>> GetFullInfoArtists(List<ArtistDto> artists) {
            for (int i = 0; i < artists.Count; i++) {
                artists[i] = await GetFullInfoArtist(artists[i]);
            }
            return artists;
        }

        public async Task<List<ArtistDto>> GetSimilarArtists(string name) {
            var artists = await client.Artist.GetSimilarAsync(name);
            return LastFmDtoMapper.MapArtists(artists);
        }

        public async Task<List<ArtistDto>> GetTopArtists(int page = 1, int itemsPerPage = 20) {
            var artists = await client.Chart.GetTopArtistsAsync(page, itemsPerPage);
            return LastFmDtoMapper.MapArtists(artists);
        }

        public async Task<List<TrackDto>> GetTopTracks(int page = 1, int itemsPerPage = 20) {
            var tracks = await client.Chart.GetTopTracksAsync(page, itemsPerPage);
            return LastFmDtoMapper.MapTracks(tracks);
        }

        public async Task<List<AlbumDto>> GetTopArtistsAlbums(string name, int page = 1, int itemsPerPage = 20) {
            var albums = await client.Artist.GetTopAlbumsAsync(name, false, page, itemsPerPage);
            return LastFmDtoMapper.MapAlbums(albums);
        }
    }
}