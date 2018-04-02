using System.Linq;
using System.Collections.Generic;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    public class SongRepository : Repository<Song, string>, ISongRepository {
        public SongRepository(ApplicationContext context) : base(context) {
        }

        public IEnumerable<Song> AddRange(IEnumerable<Song> items) {
            List<Song> songs = new List<Song>();

            foreach (var item in items) {
                songs.Add(_context.Songs.Add(item).Entity);
                _context.SaveChanges();
            }
            return songs;
        }

        public List<Song> GetAll() {
            return _context.Songs.ToList();
        }

        public List<Song> GetRange(int startIndex, int numberOfItems) {
            return _context.Songs.Skip(startIndex).Take(numberOfItems).ToList();
        }
    }
}