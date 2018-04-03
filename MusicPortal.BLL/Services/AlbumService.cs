using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.BusinessModels;
using MusicPortal.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.BLL.Services {
    public class AlbumService : IAlbumService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly LastFm _lastFm;

        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper, LastFm lastFm) {
            _database = unitOfWork;
            _mapper = mapper;
            _lastFm = lastFm;
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
    }
}