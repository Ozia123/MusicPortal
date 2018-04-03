using System.Linq;
using System.Collections.Generic;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    class ArtistRepository : Repository<Track, string>, IArtistRepository {
        public ArtistRepository(ApplicationContext context) : base(context) {
        }

        public IEnumerable<Track> AddRange(List<Track> items) {
            List<Track> artists = new List<Track>();

            foreach (var item in items) {
                artists.Add(_context.Artists.Add(item).Entity);
                _context.SaveChanges();
            }
            return artists;
        }

        public List<Track> GetAll() {
            return _context.Artists.ToList();
        }

        public List<Track> GetRange(int startIndex, int numberOfItems) {
            return _context.Artists.Skip(startIndex).Take(numberOfItems).ToList();
        }
    }
}