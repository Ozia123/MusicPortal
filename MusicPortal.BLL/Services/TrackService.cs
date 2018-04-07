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
            //await AddTracksToDatabaseIfNeeded(tracks);
            return tracks;
        }

        public async Task<List<TrackDto>> GetTopArtistsTracks(string name, int page, int itemsPerPage = 20) {
            List<TrackDto> tracks = await _lastFm.GetTopArtistsTracks(name, page, itemsPerPage);
            tracks = tracks.Skip(tracks.Count - itemsPerPage).ToList();
            //await AddTracksToDatabaseIfNeeded(tracks);
            return tracks;
        }

        private IEnumerable<TrackDto> GetTracksWhichNotInDatabase(List<TrackDto> tracks) { 
            return tracks.Where(tDto => !_database.TrackRepository.Query().Select(t => t.Name).Contains(tDto.Name));
        }

        private async Task AddTracksToDatabaseIfNeeded(List<TrackDto> tracks) {
            var tracksToAdd = GetTracksWhichNotInDatabase(tracks);
            foreach (var track in tracksToAdd) {
                await AddTrackToDatabase(track);
            }
        }

        private async Task AddTrackToDatabase(TrackDto track) {
            track = await _lastFm.GetFullInfoTrack(track.ArtistName, track.Name);
            track.AlbumId = await GetAlbumId(track.ArtistName, track.AlbumName);
            await _database.TrackRepository.Create(_mapper.Map<TrackDto, Track>(track));
        }

        private async Task<string> GetAlbumId(string artistName, string albumName) {
            Album albumFromDb = _database.AlbumRepository.GetByName(albumName);
            if (albumFromDb == null) {
                AlbumDto fullInfoAlbum = await GetAlbumFromLastFm(artistName, albumName);
                fullInfoAlbum.ArtistId = await GetArtistId(artistName);
                albumFromDb = await _database.AlbumRepository.Create(_mapper.Map<AlbumDto, Album>(fullInfoAlbum));
            }
            return albumFromDb.AlbumId;
        }

        private async Task<AlbumDto> GetAlbumFromLastFm(string artistName, string albumName) {
            return await _lastFm.GetFullInfoAlbum(artistName, albumName);
        }

        private async Task<string> GetArtistId(string artistName) {
            Artist artistFromDb = _database.ArtistRepository.GetByName(artistName);
            if (artistFromDb == null) {
                ArtistDto fullInfoArtist = await _lastFm.GetFullInfoArtist(artistName);
                artistFromDb = await _database.ArtistRepository.Create(_mapper.Map<ArtistDto, Artist>(fullInfoArtist));
            }
            return artistFromDb.ArtistId;
        }

        private async Task<ArtistDto> GetArtistFromLastFm(string artistName) {
            return await _lastFm.GetFullInfoArtist(artistName);
        }
    }
}