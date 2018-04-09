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

        public TrackService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
            _lastFm = new LastFm();
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
            tracks = tracks.Skip(tracks.Count - itemsPerPage).ToList();
            await AddTracksToDatabaseIfNeeded(tracks);
            return tracks;
        }

        public async Task<List<TrackDto>> GetTopArtistsTracks(string artistName, int page, int itemsPerPage = 20) {
            List<TrackDto> tracks = await _lastFm.GetTopArtistsTracks(artistName, page, itemsPerPage);
            tracks = tracks.Skip(tracks.Count - itemsPerPage).ToList();
            return await AddTracksToDatabaseIfNeeded(tracks);
        }

        public List<TrackDto> GetAlbumTracks(string albumName) {
            Album album = _database.AlbumRepository.GetByName(albumName);
            if (album == null) {
                return null;
            }
            List<Track> tracks = GetTracksByAlbumId(album.AlbumId).ToList();
            return _mapper.Map<List<Track>, List<TrackDto>>(tracks);
        }

        private IEnumerable<TrackDto> GetTracksWhichNotInDatabase(IEnumerable<TrackDto> tracks) {
            return tracks.Where(tDto => !_database.TrackRepository.Query().Select(t => t.Name).Contains(tDto.Name));
        }
        
        private IEnumerable<Track> GetTracksByAlbumId(string albumId) {
            return _database.TrackRepository.Query().Where(t => t.AlbumId.Equals(albumId));
        }

        private async Task<List<TrackDto>> AddTracksToDatabaseIfNeeded(List<TrackDto> tracks) {
            IEnumerable<TrackDto> tracksToAdd = GetTracksWhichNotInDatabase(tracks);
            foreach (var track in tracks) {
                await Create(track);
            }
            return tracks;
        }
    }
}
