using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.BusinessModels;
using MusicPortal.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.BLL.Services {
    public class TrackService : ITrackService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly LastFm _lastFm;
        private readonly LastFmResultFilter _resultFilter;

        public TrackService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
            _lastFm = new LastFm();
            _resultFilter = new LastFmResultFilter(_lastFm);
        }

        public IQueryable<Track> Query() {
            return _database.TrackRepository.Query();
        }

        public async Task<TrackDto> GetById(string id) {
            Track track = await _database.TrackRepository.GetById(id);
            return _mapper.Map<Track, TrackDto>(track);
        }

        public async Task<TrackDto> Create(TrackDto item) {
            Track track = _mapper.Map<TrackDto, Track>(item);
            track = await _database.TrackRepository.Create(track);
            return _mapper.Map<Track, TrackDto>(track);
        }

        public async Task<TrackDto> Update(TrackDto item) {
            Track track = _mapper.Map<TrackDto, Track>(item);
            track = await _database.TrackRepository.Update(track);
            return _mapper.Map<Track, TrackDto>(track);
        }

        public async Task<TrackDto> Delete(string id) {
            Track track = await _database.TrackRepository.Delete(id);
            return _mapper.Map<Track, TrackDto>(track);
        }

        public async Task<List<TrackDto>> GetTopTracks(int page, int itemsPerPage) {
            List<TrackDto> tracks = await _lastFm.GetTopTracks(page, itemsPerPage);
            tracks = await _resultFilter.GetFilteredTopTracks(tracks, page, itemsPerPage);
            return await GetTracksFromDatabaseAndAddIfNeeded(tracks);
        }

        public async Task<List<TrackDto>> GetTopArtistsTracks(string artistName, int page, int itemsPerPage = 20) {
            List<TrackDto> tracks = await _lastFm.GetTopArtistsTracks(artistName, page, itemsPerPage);
            tracks = await _resultFilter.GetFilteredTopArtistsTracks(tracks, artistName, page, itemsPerPage);
            return await GetTracksFromDatabaseAndAddIfNeeded(tracks);
        }

        public List<TrackDto> GetAlbumTracks(string albumName) {
            Album album = _database.AlbumRepository.GetByName(albumName);
            if (album == null) {
                return null;
            }
            List<Track> tracks = GetTracksByAlbumId(album.AlbumId).ToList();
            return _mapper.Map<List<Track>, List<TrackDto>>(tracks);
        }

        public async Task<TrackDto> UploadTrackThroughConsole(TrackDto track) {
            TrackDto lastFmTrack = await _lastFm.GetFullInfoTrack(track.ArtistName, track.Name);
            if (string.IsNullOrEmpty(lastFmTrack.Name)) {
                return null;
            }
            lastFmTrack.CloudURL = track.CloudURL;
            Track trackFromDb = await GetTrackFromDatabaseOrCreateAndGet(lastFmTrack);
            return _mapper.Map<Track, TrackDto>(trackFromDb);
        }
        
        private IEnumerable<Track> GetTracksByAlbumId(string albumId) {
            return _database.TrackRepository.Query().Where(t => t.AlbumId.Equals(albumId));
        }

        private async Task<List<TrackDto>> GetTracksFromDatabaseAndAddIfNeeded(List<TrackDto> tracks) {
            foreach (var track in tracks) {
                Track trackFromDb = await GetTrackFromDatabaseOrCreateAndGet(track);
                track.TrackId = trackFromDb.TrackId;
                track.CloudURL = trackFromDb.CloudURL;
            }
            return tracks;
        }

        private async Task<Track> GetTrackFromDatabaseOrCreateAndGet(TrackDto track) {
            Track trackFromDb = _database.TrackRepository.GetByName(track.Name);
            if (trackFromDb == null) {
                trackFromDb = await _database.TrackRepository.Create(_mapper.Map<TrackDto, Track>(track));
            }
            return trackFromDb;
        }
    }
}
