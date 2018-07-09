using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicPortal.DAL.Interfaces;
using System.Collections.Generic;
using MusicPortal.ViewModels.ViewModels;
using MusicPortal.Facade.Interfaces;

namespace MusicPortal.BLL.Services {
    public class AlbumService : IAlbumService {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;
        private readonly IMusicPortalClient musicPortalClient;

        public AlbumService(IUnitOfWork unitOfWork, IMusicPortalClient musicPortalClient, IMapper mapper) {
            database = unitOfWork;
            this.musicPortalClient = musicPortalClient;
            this.mapper = mapper;
        }

        public IQueryable<Album> Query() {
            return database.AlbumRepository.Query();
        }

        public async Task<AlbumViewModel> GetById(string id) {
            Album album = await database.AlbumRepository.GetById(id);
            return mapper.Map<Album, AlbumViewModel>(album);
        }

        public async Task<AlbumViewModel> GetByName(string name) {
            Album album = database.AlbumRepository.GetByName(name);
            AlbumViewModel albumDto = mapper.Map<Album, AlbumViewModel>(album);
            return await GetFullInfoDto(albumDto);
        }

        public async Task<AlbumViewModel> Create(AlbumViewModel item) {
            Album album = mapper.Map<AlbumViewModel, Album>(item);
            album = await database.AlbumRepository.Create(album);
            return mapper.Map<Album, AlbumViewModel>(album);
        }

        public async Task<AlbumViewModel> Update(AlbumViewModel item) {
            Album album = mapper.Map<AlbumViewModel, Album>(item);
            album = await database.AlbumRepository.Update(album);
            return mapper.Map<Album, AlbumViewModel>(album);
        }

        public async Task<AlbumViewModel> Delete(string id) {
            Album album = await database.AlbumRepository.Delete(id);
            return mapper.Map<Album, AlbumViewModel>(album);
        }

        public async Task<List<AlbumViewModel>> GetTopArtistsAlbums(string name, int page, int itemsPerPage = 20) {
            List<AlbumViewModel> albums = await musicPortalClient.GetTopArtistsAlbums(name, page, itemsPerPage);
            await AddAlbumsToDatabaseIfNeeded(albums, name);
            return albums;
        }

        private IEnumerable<AlbumViewModel> GetAlbumsWhichNotInDatabase(List<AlbumViewModel> albums) {
            return albums.Where(aDto => !database.AlbumRepository.Query().Select(a => a.Name).Contains(aDto.Name));
        }

        private IEnumerable<string> GetTrackNamesWhichNotInDatabase(IEnumerable<string> trackNames) {
            return trackNames.Where(tName => !database.TrackRepository.Query().Select(t => t.Name).Contains(tName));
        }

        private IEnumerable<Track> GetTracksWhichNotHaveAlbumIdSpecifiedInDatabase(IEnumerable<string> trackNames) {
            return database.TrackRepository.Query().Where(t => trackNames.Contains(t.Name) && t.AlbumId == null);
        }

        private async Task AddAlbumsToDatabaseIfNeeded(List<AlbumViewModel> albums, string artistName) {
            string artistId = database.ArtistRepository.GetIdByName(artistName);
            IEnumerable<AlbumViewModel> albumsToAdd = GetAlbumsWhichNotInDatabase(albums);
            await CollectInfoAboutAlbumsAndAddItAllToDatabase(albumsToAdd, artistId, artistName);
        }

        private async Task CollectInfoAboutAlbumsAndAddItAllToDatabase(IEnumerable<AlbumViewModel> albums, string artistId, string artistName) {
            foreach (var album in albums) {
                AlbumViewModel albumFromDb = await AddAlbumToDatabase(album, artistId);
                AlbumViewModel fullInfoAlbum = await musicPortalClient.GetFullInfoAlbum(artistName, albumFromDb.Name);
                await UpdateTracksInDatabaseIfNeeded(fullInfoAlbum.TrackNames, albumFromDb.AlbumId);
                await AddTracksToDatabaseIfNeeded(fullInfoAlbum.TrackNames, albumFromDb.AlbumId);
            }
        }

        private async Task AddTracksToDatabaseIfNeeded(IEnumerable<string> trackNames, string albumId) {
            IEnumerable<string> trackNamesToAdd = GetTrackNamesWhichNotInDatabase(trackNames);
            foreach (var trackName in trackNames) {
                TrackViewModel trackToAdd = GetTrackDto(trackName, albumId);
                await database.TrackRepository.Create(mapper.Map<TrackViewModel, Track>(trackToAdd));
            }
        }

        private async Task UpdateTracksInDatabaseIfNeeded(IEnumerable<string> trackNames, string albumId) {
            IEnumerable<Track> tracksToUpdate = GetTracksWhichNotHaveAlbumIdSpecifiedInDatabase(trackNames);
            tracksToUpdate = tracksToUpdate.Select(t => { t.AlbumId = albumId; return t; });
            await database.TrackRepository.UpdateRange(tracksToUpdate);
        }

        private async Task<AlbumViewModel> AddAlbumToDatabase(AlbumViewModel album, string artistId) {
            album.ArtistId = artistId;
            return await Create(album);
        }

        private async Task<AlbumViewModel> GetFullInfoDto(AlbumViewModel albumDto) {
            Artist artist = await database.ArtistRepository.GetById(albumDto.ArtistId);
            albumDto.ArtistName = artist.Name;
            return albumDto;
        }

        private TrackViewModel GetTrackDto(string trackName, string albumId) {
            return new TrackViewModel {
                Name = trackName,
                AlbumId = albumId
            };
        }
    }
}