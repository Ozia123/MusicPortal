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
using MusicPortal.BLL.Base.Implementation;

namespace MusicPortal.BLL.Services {
    public class ArtistService : Service<ArtistViewModel, Artist, string>, IArtistService {
        private readonly IMusicPortalClient musicPortalClient;

        public ArtistService(IUnitOfWork unitOfWork, IMusicPortalClient musicPortalClient, IMapper mapper)
            : base(mapper, unitOfWork)
        {
            this.musicPortalClient = musicPortalClient;
        }

        public async Task<ArtistViewModel> GetByName(string name) {
            var artist = await GetArtistFromDatabaseOrIfNotFoundFromLastFmByName(name);
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