using AutoMapper;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;

namespace MusicPortal.BLL.UnitTests.Factories {
    public static class ServiceFactory {
        private static readonly IMapper mapper;
        private static readonly ApplicationContext context;
        private static readonly IUnitOfWork database;

        static ServiceFactory() {
            mapper = MapperFactory.GetMapper();
            context = ContextFactory.GetContext();
            database = new UnitOfWork(context);
        }

        public static IArtistService GetArtistService() {
            var musicPortalClient = FacadeFactory.GetMusicPortalClient();
            return new ArtistService(database, musicPortalClient, mapper);
        }

        public static IAlbumService GetAlbumService() {
            var musicPortalClient = FacadeFactory.GetMusicPortalClient();
            return new AlbumService(database, musicPortalClient, mapper);
        }

        public static ITrackService GetTrackService() {
            var musicPortalClient = FacadeFactory.GetMusicPortalClient();
            return new TrackService(database, musicPortalClient, mapper, GetArtistService());
        }
    }
}
