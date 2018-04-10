using MusicPortal.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.BLL.BusinessModels {
    public class LastFmResultFilter {
        private readonly LastFm _lastFm;

        public LastFmResultFilter(LastFm lastFm) {
            _lastFm = lastFm;
        }

        private async Task<List<TrackDto>> GetPreviousPageResultOfTopTracks(int page, int itemsPerPage) {
            return await _lastFm.GetTopTracks(page - 1, itemsPerPage);
        }

        private async Task<List<TrackDto>> GetPreviousPageResultOfTopArtistsTracks(string artistName, int page, int itemsPerPage) {
            return await _lastFm.GetTopArtistsTracks(artistName, page - 1, itemsPerPage);
        }

        private List<TrackDto> CompareTracksWithPreviousPageResult(List<TrackDto> tracks, List<TrackDto> previousPageResult) {
            bool isSameDataFound = previousPageResult.Exists(t => tracks.Contains(t));
            if (isSameDataFound) {
                return new List<TrackDto>();
            }
            return tracks;
        }

        public async Task<List<TrackDto>> GetFilteredTopTracks(List<TrackDto> tracks, int page, int itemsPerPage) {
            tracks = tracks.Skip(tracks.Count - itemsPerPage).ToList();
            if (page != 1) {
                List<TrackDto> previousPageResult = await GetPreviousPageResultOfTopTracks(page, itemsPerPage);
                tracks = CompareTracksWithPreviousPageResult(tracks, previousPageResult);
            }
            return tracks;
        }

        public async Task<List<TrackDto>> GetFilteredTopArtistsTracks(List<TrackDto> tracks, string artistName, int page, int itemsPerPage) {
            tracks = tracks.Skip(tracks.Count - itemsPerPage).ToList();
            if (page != 1) {
                List<TrackDto> previousPageResult = await GetPreviousPageResultOfTopArtistsTracks(artistName, page, itemsPerPage);
                tracks = CompareTracksWithPreviousPageResult(tracks, previousPageResult);
            }
            return tracks;
        }
    }
}
