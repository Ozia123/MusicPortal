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

        private async Task<string> GetArtistBio(string mbid) {
            LastArtist artist = (await client.Artist.GetInfoByMbidAsync(mbid)).Content ?? new LastArtist();
            artist.Bio = artist.Bio ?? new LastWiki();
            return artist.Bio.Content ?? "no bio";
        }

        private async Task<LastArtist> GetFullInfoArtist(LastArtist artist) {
            if (artist.Bio == null) {
                artist.Bio = new LastWiki();
            }
            artist.Bio.Content = await GetArtistBio(artist.Mbid);
            return artist;
        }

        public async Task<List<LastArtist>> GetFullInfoArtists(PageResponse<LastArtist> artists) {
            List<LastArtist> fullInfoArtists = new List<LastArtist>();
            for (int i = 0; i < artists.Content.Count; i++) {
                fullInfoArtists.Add(await GetFullInfoArtist(artists.Content[i]));
            }
            return fullInfoArtists;
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
    }
}