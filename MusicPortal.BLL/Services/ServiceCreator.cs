using AutoMapper;
using MusicPortal.BLL.BusinessModels;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Repositories;

namespace MusicPortal.BLL.Services {
    class ServiceCreator : IServiceCreator {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ServiceCreator(
            ApplicationContext context,
            IMapper mapper
        ) {
            _context = context;
            _mapper = mapper;
        }

        public UnitOfWork CreateUnitOfWork() {
            return new UnitOfWork(_context);
        }

        public IArtistService CreateArtistService() {
            return new ArtistService(CreateUnitOfWork(), _mapper);
        }

        public IAlbumService CreateAlbumService() {
            return new AlbumService(CreateUnitOfWork(), _mapper, new LastFm());
        }

        public ITrackService CreateTrackService() {
            return new TrackService(CreateUnitOfWork(), _mapper);
        }
    }
}