using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.BusinessModels;
using MusicPortal.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicPortal.DAL.Interfaces;
using System.Collections.Generic;

namespace MusicPortal.BLL.Services {
    public class AlbumService : IAlbumService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly LastFm _lastFm;

        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
            _lastFm = new LastFm();
        }

        public IQueryable<Album> Query() {
            return _database.AlbumRepository.Query();
        }

        public async Task<AlbumDto> GetById(string id) {
            Album album = await _database.AlbumRepository.GetById(id);
            return _mapper.Map<Album, AlbumDto>(album);
        }

        public async Task<AlbumDto> Create(AlbumDto item) {
            Album album = _mapper.Map<AlbumDto, Album>(item);
            album = await _database.AlbumRepository.Create(album);
            return _mapper.Map<Album, AlbumDto>(album);
        }

        public async Task<AlbumDto> Update(AlbumDto item) {
            Album album = _mapper.Map<AlbumDto, Album>(item);
            album = await _database.AlbumRepository.Update(album);
            return _mapper.Map<Album, AlbumDto>(album);
        }

        public async Task<AlbumDto> Delete(string id) {
            Album album = await _database.AlbumRepository.Delete(id);
            return _mapper.Map<Album, AlbumDto>(album);
        }

        public async Task<List<AlbumDto>> GetTopArtistsAlbums(string name, int page, int itemsPerPage = 20) {
            List<AlbumDto> albums = await _lastFm.GetTopArtistsAlbums(name, page, itemsPerPage);
            albums = albums.Skip(albums.Count - itemsPerPage).ToList();
            await AddAlbumsToDatabaseIfNeeded(albums, name);
            return albums;
        }

        private async Task AddAlbumsToDatabaseIfNeeded(List<AlbumDto> albums, string artistName) {
            string artistId = GetArtistIdByName(artistName);
            var albumsToAdd = GetAlbumsWhichNotInDatabase(albums);
            await GetFullInfoAndAddToDatabase(albumsToAdd, artistId);
        }

        private IEnumerable<AlbumDto> GetAlbumsWhichNotInDatabase(List<AlbumDto> albums) {
            return albums.Where(aDto => !_database.AlbumRepository.Query().Select(a => a.Name).Contains(aDto.Name));
        }

        private string GetArtistIdByName(string name) {
            Artist artist = _database.ArtistRepository.GetByName(name);
            if (artist == null) {
                return _database.ArtistRepository.Query().FirstOrDefault(a => true).ArtistId;
            }
            return artist.ArtistId;
        }

        private async Task GetFullInfoAndAddToDatabase(IEnumerable<AlbumDto> albums, string artistId) {
            foreach (var album in albums) {
                Album albumFromDb = await AddAlbumToDatabase(album, artistId);
                await GetAlbumsTracksAndAddThemToDatabase(albumFromDb, album.ArtistName);
            }
        }

        private async Task<Album> AddAlbumToDatabase(AlbumDto album, string artistId) {
            album.ArtistId = artistId;
            return await _database.AlbumRepository.Create(_mapper.Map<AlbumDto, Album>(album));
        }

        private async Task GetAlbumsTracksAndAddThemToDatabase(Album albumFromDb, string artistName) {
            AlbumDto fullInfoAlbum = await _lastFm.GetFullInfoAlbum(artistName, albumFromDb.Name);
            IEnumerable<string> trackNamesToAdd = GetTrackNamesWhichNotInDatabase(fullInfoAlbum.TrackNames);
            await AddTracksToDatabase(trackNamesToAdd, artistName, albumFromDb.AlbumId);
        }

        private async Task AddTracksToDatabase(IEnumerable<string> trackNames, string artistName, string albumId) {
            foreach (var trackName in trackNames) {
                TrackDto trackToAdd = GetTrackDto(artistName, trackName, albumId);
                await _database.TrackRepository.Create(_mapper.Map<TrackDto, Track>(trackToAdd));
            }
        }

        private IEnumerable<string> GetTrackNamesWhichNotInDatabase(IEnumerable<string> trackNames) {
            return trackNames.Where(tName => _database.TrackRepository.Query().Select(t => t.Name).Contains(tName));
        }

        private TrackDto GetTrackDto(string artistName, string trackName, string albumId) {
            return new TrackDto {
                Name = trackName,
                AlbumId = albumId
            };
        }
    }
}