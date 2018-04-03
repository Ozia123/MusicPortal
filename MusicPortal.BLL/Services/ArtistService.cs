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

        public IQueryable<Track> Query() {
            return _database.ArtistRepository.Query();
        }

        public async Task<TrackDto> GetById(string id) {
            Track artist = await _database.ArtistRepository.GetById(id);
            return _mapper.Map<Track, TrackDto>(artist);
        }

        public async Task<TrackDto> Create(TrackDto item) {
            Track artist = _mapper.Map<TrackDto, Track>(item);
            artist = await _database.ArtistRepository.Create(artist);
            return _mapper.Map<Track, TrackDto>(artist);
        }

        public async Task<TrackDto> Update(TrackDto item) {
            Track artist = _mapper.Map<TrackDto, Track>(item);
            artist = await _database.ArtistRepository.Update(artist);
            return _mapper.Map<Track, TrackDto>(artist);
        }

        public async Task<TrackDto> Delete(string id) {
            Track artist = await _database.ArtistRepository.Delete(id);
            return _mapper.Map<Track, TrackDto>(artist);
        }

        public async Task<List<TrackDto>> GetTopArtists(int page, int itemsPerPage) {
            List<TrackDto> artists = await _lastFm.GetTopArtists(page, itemsPerPage);
            return artists;
        }
    }
}