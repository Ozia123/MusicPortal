using IF.Lastfm.Core.Api;
using IF.Lastfm.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Facade.Helpers {
    public class LastFmResultFilter {
        private readonly LastfmClient client;

        public LastFmResultFilter(LastfmClient lastFm) {
            client = lastFm;
        }

        private async Task<List<LastTrack>> GetPreviousPageResultOfTopTracks(int page, int itemsPerPage) {
            var response = await client.Chart.GetTopTracksAsync(page - 1, itemsPerPage);
            return response.Select(t => t).ToList();
        }

        private async Task<List<LastTrack>> GetPreviousPageResultOfTopArtistsTracks(string artistName, int page, int itemsPerPage) {
            var response = await client.Artist.GetTopTracksAsync(artistName, false, page - 1, itemsPerPage);
            return response.Select(t => t).ToList();
        }

        private async Task<List<LastAlbum>> GetPreviousPageResultOfTopArtistsAlbums(string artistName, int page, int itemsPerPage) {
            var response = await client.Artist.GetTopAlbumsAsync(artistName, false, page - 1, itemsPerPage);
            return response.Select(a => a).ToList();
        }

        private List<LastTrack> CompareTracksWithPreviousPageResult(List<LastTrack> tracks, List<LastTrack> previousPageResult) {
            return tracks.Where(t => !previousPageResult.Select(pt => pt.Name).Contains(t.Name)).ToList();
        }

        private List<LastAlbum> CompareAlbumsWithPreviousPageResult(List<LastAlbum> albums, List<LastAlbum> previousPageResult) {
            return albums.Where(t => !previousPageResult.Select(pt => pt.Name).Contains(t.Name)).ToList();
        }

        public async Task<List<LastTrack>> GetFilteredTopTracks(List<LastTrack> tracks, int page, int itemsPerPage) {
            tracks = tracks.Skip(tracks.Count - itemsPerPage).ToList();
            if (page != 1) {
                List<LastTrack> previousPageResult = await GetPreviousPageResultOfTopTracks(page, itemsPerPage);
                tracks = CompareTracksWithPreviousPageResult(tracks, previousPageResult);
            }
            return tracks;
        }

        public async Task<List<LastTrack>> GetFilteredTopArtistsTracks(List<LastTrack> tracks, string artistName, int page, int itemsPerPage) {
            tracks = tracks.Skip(tracks.Count - itemsPerPage).ToList();
            if (page != 1) {
                List<LastTrack> previousPageResult = await GetPreviousPageResultOfTopArtistsTracks(artistName, page, itemsPerPage);
                tracks = CompareTracksWithPreviousPageResult(tracks, previousPageResult);
            }
            return tracks;
        }

        public async Task<List<LastAlbum>> GetFilteredTopArtistsAlbums(List<LastAlbum> albums, string artistName, int page, int itemsPerPage) {
            albums = albums.Skip(albums.Count - itemsPerPage).ToList();
            if (page != 1) {
                List<LastAlbum> previousPageResult = await GetPreviousPageResultOfTopArtistsAlbums(artistName, page, itemsPerPage);
                albums = CompareAlbumsWithPreviousPageResult(albums, previousPageResult);
            }
            return albums;
        }
    }
}
