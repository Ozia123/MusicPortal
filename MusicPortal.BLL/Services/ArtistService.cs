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
    public class ArtistService : IArtistService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly LastFm _lastFm;

        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper, LastFm lastFm) {
            _database = unitOfWork;
            _mapper = mapper;
            _lastFm = lastFm;
        }

        public IQueryable<Artist> Query() {
            return _database.ArtistRepository.Query();
        }

        public async Task<ArtistDto> GetById(string id) {
            Artist artist = await _database.ArtistRepository.GetById(id);
            return _mapper.Map<Artist, ArtistDto>(artist);
        }

        public async Task<ArtistDto> Create(ArtistDto item) {
            Artist artist = _mapper.Map<ArtistDto, Artist>(item);
            artist = await _database.ArtistRepository.Create(artist);
            return _mapper.Map<Artist, ArtistDto>(artist);
        }

        public async Task<ArtistDto> Update(ArtistDto item) {
            Artist artist = _mapper.Map<ArtistDto, Artist>(item);
            artist = await _database.ArtistRepository.Update(artist);
            return _mapper.Map<Artist, ArtistDto>(artist);
        }

        public async Task<ArtistDto> Delete(string id) {
            Artist artist = await _database.ArtistRepository.Delete(id);
            return _mapper.Map<Artist, ArtistDto>(artist);
        }

        public async Task<List<ArtistDto>> GetTopArtists(int page, int itemsPerPage) {
            List<ArtistDto> artists = await _lastFm.GetTopArtists(page, itemsPerPage);
            return artists;
        }
    }
}