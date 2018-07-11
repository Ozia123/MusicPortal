using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicPortal.DAL.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using MusicPortal.Facade.Interfaces;
using MusicPortal.BLL.Base.Implementation;

namespace MusicPortal.BLL.Services {
    public class TrackService : Service<TrackViewModel, Track, string>, ITrackService {
        private readonly IMusicPortalClient musicPortalClient;
        private readonly IArtistService artistService;

        public TrackService(IUnitOfWork unitOfWork, IMusicPortalClient musicPortalClient, IMapper mapper, IArtistService artistService)
            : base(mapper, unitOfWork)
        {
            this.musicPortalClient = musicPortalClient;
            this.artistService = artistService;
        }

        public async Task<List<TrackViewModel>> GetTopTracks(int page, int itemsPerPage) {
            List<TrackViewModel> tracks = await musicPortalClient.GetTopTracks(page, itemsPerPage);
            return await GetTracksFromDatabaseAndAddIfNeeded(tracks);
        }

        public async Task<List<TrackViewModel>> GetTopArtistsTracks(string artistName, int page, int itemsPerPage = 20) {
            List<TrackViewModel> tracks = await musicPortalClient.GetTopArtistsTracks(artistName, page, itemsPerPage);
            return await GetTracksFromDatabaseAndAddIfNeeded(tracks);
        }

        public async Task<List<TrackViewModel>> GetAlbumTracks(string albumName) {
            Album album = await database.AlbumRepository.GetByName(albumName);
            if (album == null) {
                return null;
            }
            List<Track> tracks = GetTracksByAlbumId(album.AlbumId).ToList();
            return mapper.Map<List<Track>, List<TrackViewModel>>(tracks);
        }

        public async Task<TrackViewModel> UploadTrackThroughConsole(TrackViewModel track) {
            TrackViewModel lastFmTrack = await musicPortalClient.GetFullInfoTrack(track.ArtistName, track.Name);
            if (string.IsNullOrEmpty(lastFmTrack.Name)) {
                return null;
            }
            lastFmTrack.CloudURL = track.CloudURL;
            return await UpdateTrackInDatabaseOrCreateIfNeeded(lastFmTrack);
        }

        private IEnumerable<Track> GetTracksByAlbumId(string albumId) {
            return database.TrackRepository.Query().Where(t => t.AlbumId.Equals(albumId));
        }

        private async Task<List<TrackViewModel>> GetTracksFromDatabaseAndAddIfNeeded(List<TrackViewModel> tracks) {
            List<Track> tracksFromDb = new List<Track>();
            var grouppedByArtist = tracks.GroupBy(x => x.ArtistName);

            foreach (var artistTracks in grouppedByArtist) {
                var artist = await artistService.GetArtistFromDatabaseOrIfNotFoundFromLastFmByName(artistTracks.Key);
                var tracksToCheck = artistTracks.Select(track => { track.ArtistId = artist.ArtistId; return track; }).ToList();
                tracksFromDb.AddRange(await GetTracksFromDatabaseOrCreateIfNeeded(tracksToCheck));
            }
            
            return mapper.Map<List<TrackViewModel>>(tracksFromDb.OrderByDescending(x => x.Rank).ToList());
        }

        private async Task<List<Track>> GetTracksFromDatabaseOrCreateIfNeeded(List<TrackViewModel> tracks) {
            List<Track> tracksFromDb = new List<Track>();

            foreach (var track in tracks) {
                Track trackFromDb = await database.TrackRepository.GetByName(track.Name);
                if (trackFromDb == null) {
                    trackFromDb = await database.TrackRepository.Create(mapper.Map<TrackViewModel, Track>(track));
                }
                tracksFromDb.Add(trackFromDb);
            }

            return tracksFromDb;
        }

        private async Task<string> GetTrackIdFromDatabaseOrCreateAndGet(TrackViewModel track) {
            Track trackFromDb = await database.TrackRepository.GetByName(track.Name);
            if (trackFromDb == null) {
                
                trackFromDb = await database.TrackRepository.Create(mapper.Map<TrackViewModel, Track>(track));
            }
            return trackFromDb.TrackId;
        }

        private async Task<TrackViewModel> UpdateTrackInDatabaseOrCreateIfNeeded(TrackViewModel track) {
            Track trackFromDb = await database.TrackRepository.GetByName(track.Name);
            if (trackFromDb == null) {
                trackFromDb = await database.TrackRepository.Create(mapper.Map<TrackViewModel, Track>(track));
                return mapper.Map<Track, TrackViewModel>(trackFromDb);
            }

            return await UpdateIfNeeded(trackFromDb, track.CloudURL);
        }

        public async Task<TrackViewModel> UpdateIfNeeded(Track trackFromDb, string cloudURL) {
            if (string.IsNullOrEmpty(trackFromDb.CloudURL)) {
                trackFromDb.CloudURL = cloudURL;
                await database.TrackRepository.Update(trackFromDb);
            }
            return mapper.Map<Track, TrackViewModel>(trackFromDb);
        }
    }
}
