using System.Linq;
using System.Collections.Generic;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    public class AlbumRepository : Repository<Album, string>, IAlbumRepository {
        public AlbumRepository(ApplicationContext context) : base(context) {
        }

        public IEnumerable<Album> AddRange(IEnumerable<Album> items) {
            List<Album> albums = new List<Album>();

            foreach (var item in items) {
                albums.Add(_context.Albums.Add(item).Entity);
                _context.SaveChanges();
            }
            return albums;
        }

        public List<Album> GetAll() {
            return _context.Albums.ToList();
        }

        public Album GetByName(string name) {
            return _context.Albums.FirstOrDefault(a => a.Name.Equals(name));
        }

        public List<Album> GetRange(int startIndex, int numberOfItems) {
            return _context.Albums.Skip(startIndex).Take(numberOfItems).ToList();
        }
    }
}