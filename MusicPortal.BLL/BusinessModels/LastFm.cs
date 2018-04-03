using IF.Lastfm.Core.Api;
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