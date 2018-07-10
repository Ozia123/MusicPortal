using AutoMapper;
using IF.Lastfm.Core.Api;
using IF.Lastfm.Core.Objects;
using Microsoft.Extensions.Configuration;
using MusicPortal.Facade.Helpers;
using MusicPortal.Facade.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Facade.Facades {
    public class MusicPortalClient: IMusicPortalClient {
        private readonly IMapper mapper;
        private readonly LastfmClient client;
        private readonly LastFmResultFilter resultFilter;

        public MusicPortalClient(IMapper mapper, IConfiguration configuration) {
            var apiKey = configuration.GetValue<string>("LastFmApiKey");
            var apiSecret = configuration.GetValue<string>("LastFmApiSecret");

            client = new LastfmClient(apiKey, apiSecret);
            resultFilter = new LastFmResultFilter(client);

            this.mapper = mapper;
        }

        public async Task<ArtistViewModel> GetFullInfoArtist(string artistName) {
            var fullInfoArtist = (await client.Artist.GetInfoAsync(artistName)).Content ?? new LastArtist();
            return mapper.Map<ArtistViewModel>(fullInfoArtist);
        }

        public async Task<AlbumViewModel> GetFullInfoAlbum(string artistName, string albumName) {
            var fullInfoAlbum = await client.Album.GetInfoAsync(artistName, albumName, false);
            return mapper.Map<AlbumViewModel>(fullInfoAlbum.Content);
        }

        public async Task<TrackViewModel> GetFullInfoTrack(string artistName, string trackName) {
            var fullInfoTrack = (await client.Track.GetInfoAsync(trackName, artistName)).Content;
            return mapper.Map<TrackViewModel>(fullInfoTrack);
        }

        public async Task<List<ArtistViewModel>> GetTopArtists(int page = 1, int itemsPerPage = 20) {
            var artists = await client.Chart.GetTopArtistsAsync(page, itemsPerPage);
            return mapper.Map<List<ArtistViewModel>>(artists.Select(a => a).ToList());
        }

        public async Task<List<ArtistViewModel>> GetSimilarArtists(string name) {
            var artists = await client.Artist.GetSimilarAsync(name);
            return mapper.Map<List<ArtistViewModel>>(artists.Select(a => a).ToList());
        }

        public async Task<List<TrackViewModel>> GetTopTracks(int page = 1, int itemsPerPage = 20) {
            var response = await client.Chart.GetTopTracksAsync(page, itemsPerPage);
            var tracks = await resultFilter.GetFilteredTopTracks(response.Select(t => t).ToList(), page, itemsPerPage);

            return mapper.Map<List<TrackViewModel>>(tracks);
        }

        public async Task<List<AlbumViewModel>> GetTopArtistsAlbums(string name, int page = 1, int itemsPerPage = 20) {
            var response = await client.Artist.GetTopAlbumsAsync(name, false, page, itemsPerPage);
            var albums = await resultFilter.GetFilteredTopArtistsAlbums(response.Where(x => x.Name != "(null)").ToList(), name, page, itemsPerPage);

            return mapper.Map<List<AlbumViewModel>>(albums);
        }

        public async Task<List<TrackViewModel>> GetTopArtistsTracks(string name, int page = 1, int itemsPerPage = 20) {
            var response = await client.Artist.GetTopTracksAsync(name, false, page, itemsPerPage);
            var tracks = await resultFilter.GetFilteredTopArtistsTracks(response.Select(t => t).ToList(), name, page, itemsPerPage);

            return mapper.Map<List<TrackViewModel>>(tracks);
        }
    }
}
