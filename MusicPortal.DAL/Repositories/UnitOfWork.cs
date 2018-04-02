﻿using System.Threading.Tasks;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationContext _context;

        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly ISongRepository _songRepository;

        public UnitOfWork(ApplicationContext context) {
            _context = context;

            _artistRepository = new ArtistRepository(_context);
            _albumRepository = new AlbumRepository(_context);
            _songRepository = new SongRepository(_context);
        }

        public ApplicationContext Context => _context;

        public IArtistRepository ArtistRepository => _artistRepository;

        public IAlbumRepository AlbumRepository => _albumRepository;

        public ISongRepository SongRepository => _songRepository;

        public async Task SaveAsync() {
            await _context.SaveChangesAsync();
        }
    }
}