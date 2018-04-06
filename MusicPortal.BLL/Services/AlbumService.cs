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
            await AddAlbumsToDatabase(albums, name);
            return albums;
        }

        private async Task AddAlbumsToDatabase(List<AlbumDto> albums, string artistName) {
            string artistId = GetArtistIdByName(artistName);
            foreach (var album in albums) {
                await AddAlbumToDatabaseIfNotExists(album, artistId);
            }
        }

        private string GetArtistIdByName(string name) {
            Artist artist = _database.ArtistRepository.GetByName(name);
            if (artist == null) {
                return _database.ArtistRepository.Query().FirstOrDefault(a => true).ArtistId;
            }
            return artist.ArtistId;
        }

        private async Task AddAlbumToDatabaseIfNotExists(AlbumDto album, string artistId) {
            Album albumFromDb = _database.AlbumRepository.GetByName(album.Name);
            if (albumFromDb == null) {
                await AddAlbumToDatabase(album, artistId);
            }
        }

        private async Task AddAlbumToDatabase(AlbumDto album, string artistId) {
            album.ArtistId = artistId;
            await _database.AlbumRepository.Create(_mapper.Map<AlbumDto, Album>(album));
        }
    }
}