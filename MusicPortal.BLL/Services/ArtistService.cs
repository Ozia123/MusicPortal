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

        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
            _lastFm = new LastFm();
        }

        public IQueryable<Artist> Query() {
            return _database.ArtistRepository.Query();
        }

        public async Task<ArtistDto> GetById(string id) {
            Artist artist = await _database.ArtistRepository.GetById(id);
            return _mapper.Map<Artist, ArtistDto>(artist);
        }

        public ArtistDto GetByName(string name) {
            Artist artist = _database.ArtistRepository.GetByName(name);
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
            artists = artists.Skip(artists.Count - itemsPerPage).ToList();
            await GetFullInfoAndAddToDatabaseIfNeeded(artists);
            return artists;
        }

        public async Task<List<ArtistDto>> GetSimilarArtists(string name) {
            List<ArtistDto> artists = await _lastFm.GetSimilarArtists(name);
            await GetFullInfoAndAddToDatabaseIfNeeded(artists);
            return artists;
        }

        public List<ArtistDto> GetArtistWhichPictureURLContainsThreeA() {
            List<Artist> artists = _database.ArtistRepository.Query().Where(a => a.PictureURL.Contains("aaa")).ToList();
            return _mapper.Map<List<Artist>, List<ArtistDto>>(artists);
        }

        private IEnumerable<ArtistDto> GetArtistsWhichNotInDatabase(IEnumerable<ArtistDto> artists) {
            return artists.Where(aDto => !_database.ArtistRepository.Query().Select(a => a.Name).Contains(aDto.Name));
        }

        private async Task GetFullInfoAndAddToDatabaseIfNeeded(List<ArtistDto> artists) {
            IEnumerable<ArtistDto> artistsToAdd = GetArtistsWhichNotInDatabase(artists);
            foreach (var artist in artistsToAdd) {
                ArtistDto artistToAdd = await GetFullInfoAboutArtist(artist);
                await Create(artistToAdd);
            }
        }

        private async Task<ArtistDto> GetFullInfoAboutArtist(ArtistDto artist) {
            return await _lastFm.GetFullInfoArtist(artist.Name);
        }
    }
}