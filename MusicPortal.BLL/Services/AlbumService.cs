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

        public async Task<AlbumDto> GetByName(string name) {
            Album album = _database.AlbumRepository.GetByName(name);
            AlbumDto albumDto = _mapper.Map<Album, AlbumDto>(album);
            return await GetFullInfoDto(albumDto);
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

        private IEnumerable<AlbumDto> GetAlbumsWhichNotInDatabase(List<AlbumDto> albums) {
            return albums.Where(aDto => !_database.AlbumRepository.Query().Select(a => a.Name).Contains(aDto.Name));
        }

        private IEnumerable<string> GetTrackNamesWhichNotInDatabase(IEnumerable<string> trackNames) {
            return trackNames.Where(tName => !_database.TrackRepository.Query().Select(t => t.Name).Contains(tName));
        }

        private async Task<AlbumDto> GetFullInfoDto(AlbumDto albumDto) {
            Artist artist = await _database.ArtistRepository.GetById(albumDto.ArtistId);
            albumDto.ArtistName = artist.Name;
            return albumDto;
        }

        private async Task AddAlbumsToDatabaseIfNeeded(List<AlbumDto> albums, string artistName) {
            string artistId = _database.ArtistRepository.GetIdByName(artistName);
            IEnumerable<AlbumDto> albumsToAdd = GetAlbumsWhichNotInDatabase(albums);
            foreach (var album in albumsToAdd) {
                AlbumDto albumFromDb = await AddAlbumToDatabase(album, artistId);
                await GetAlbumsTracksAndAddThemToDatabase(albumFromDb, album.ArtistName);
            }
        }

        private async Task<AlbumDto> AddAlbumToDatabase(AlbumDto album, string artistId) {
            album.ArtistId = artistId;
            return await Create(album);
        }

        private async Task GetAlbumsTracksAndAddThemToDatabase(AlbumDto albumFromDb, string artistName) {
            AlbumDto fullInfoAlbum = await _lastFm.GetFullInfoAlbum(artistName, albumFromDb.Name);
            IEnumerable<string> trackNamesToAdd = GetTrackNamesWhichNotInDatabase(fullInfoAlbum.TrackNames);
            foreach (var trackName in trackNamesToAdd) {
                TrackDto trackToAdd = GetTrackDto(artistName, trackName, albumFromDb.AlbumId);
                await _database.TrackRepository.Create(_mapper.Map<TrackDto, Track>(trackToAdd));
            }
        }

        private TrackDto GetTrackDto(string artistName, string trackName, string albumId) {
            return new TrackDto {
                Name = trackName,
                ArtistName = artistName,
                AlbumId = albumId
            };
        }
    }
}