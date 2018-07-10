using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicPortal.DAL.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using MusicPortal.Facade.Interfaces;
using System;

namespace MusicPortal.BLL.Services {
    public class ArtistService : IArtistService {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;
        private readonly IMusicPortalClient musicPortalClient;

        public ArtistService(IUnitOfWork unitOfWork, IMusicPortalClient musicPortalClient, IMapper mapper) {
            database = unitOfWork;
            this.mapper = mapper;
            this.musicPortalClient = musicPortalClient;
        }

        public IQueryable<Artist> Query() {
            return database.ArtistRepository.Query();
        }

        public async Task<ArtistViewModel> GetById(string id) {
            Artist artist = await database.ArtistRepository.GetById(id);
            return mapper.Map<Artist, ArtistViewModel>(artist);
        }

        public async Task<ArtistViewModel> GetByName(string name) {
            var artist = await GetArtistFromDatabaseOrIfNotFoundFromLastFmByName(name);
            return mapper.Map<Artist, ArtistViewModel>(artist);
        }

        public async Task<ArtistViewModel> Create(ArtistViewModel item) {
            Artist artist = mapper.Map<ArtistViewModel, Artist>(item);
            artist = await database.ArtistRepository.Create(artist);
            return mapper.Map<Artist, ArtistViewModel>(artist);
        }

        public async Task<ArtistViewModel> Update(ArtistViewModel item) {
            Artist artist = mapper.Map<ArtistViewModel, Artist>(item);
            artist = await database.ArtistRepository.Update(artist);
            return mapper.Map<Artist, ArtistViewModel>(artist);
        }

        public async Task<ArtistViewModel> Delete(string id) {
            Artist artist = await database.ArtistRepository.Remove(id);
            return mapper.Map<Artist, ArtistViewModel>(artist);
        }

        public async Task<List<ArtistViewModel>> GetTopArtists(int page, int itemsPerPage) {
            List<ArtistViewModel> artists = await musicPortalClient.GetTopArtists(page, itemsPerPage);
            artists = artists.Skip(artists.Count - itemsPerPage).ToList();
            await GetFullInfoAndAddToDatabaseIfNeeded(artists);
            return artists;
        }

        public async Task<List<ArtistViewModel>> GetSimilarArtists(string name) {
            List<ArtistViewModel> artists = await musicPortalClient.GetSimilarArtists(name);
            await GetFullInfoAndAddToDatabaseIfNeeded(artists);
            return artists;
        }
        
        public async Task<Artist> GetArtistFromDatabaseOrIfNotFoundFromLastFmByName(string name) {
            var artist = await database.ArtistRepository.GetByName(name);

            if (artist == null) {
                var artistFromLastFm = await musicPortalClient.GetFullInfoArtist(name);
                artist = await database.ArtistRepository.Create(mapper.Map<Artist>(artistFromLastFm));
            }
            return artist;
        }

        private IEnumerable<ArtistViewModel> GetArtistsWhichNotInDatabase(IEnumerable<ArtistViewModel> artists) {
            return artists.Where(aDto => !database.ArtistRepository.Query().Select(a => a.Name).Contains(aDto.Name));
        }

        private async Task GetFullInfoAndAddToDatabaseIfNeeded(List<ArtistViewModel> artists) {
            IEnumerable<ArtistViewModel> artistsToAdd = GetArtistsWhichNotInDatabase(artists);
            foreach (var artist in artistsToAdd) {
                ArtistViewModel artistToAdd = await GetFullInfoAboutArtist(artist);
                await Create(artistToAdd);
            }
        }

        private async Task<ArtistViewModel> GetFullInfoAboutArtist(ArtistViewModel artist) {
            return await musicPortalClient.GetFullInfoArtist(artist.Name);
        }

        private async Task<ArtistViewModel> TryToGetArtistFromLastFm(string name) {
            var artistFromLastFm = await musicPortalClient.GetFullInfoArtist(name);

            if (artistFromLastFm != null) {
                return await Create(artistFromLastFm);
            }

            return null;
        }
    }
}