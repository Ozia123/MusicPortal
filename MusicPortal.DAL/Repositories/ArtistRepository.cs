using System.Linq;
using System.Collections.Generic;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    class ArtistRepository : Repository<Artist, string>, IArtistRepository {
        public ArtistRepository(ApplicationContext context) : base(context) {
        }

        public IEnumerable<Artist> AddRange(List<Artist> items) {
            List<Artist> artists = new List<Artist>();

            foreach (var item in items) {
                artists.Add(_context.Artists.Add(item).Entity);
                _context.SaveChanges();
            }
            return artists;
        }

        public List<Artist> GetAll() {
            return _context.Artists.ToList();
        }

        public Artist GetByName(string name) {
            return _context.Artists.FirstOrDefault(a => a.Name.Equals(name));
        }

        public string GetIdByName(string name) {
            return _context.Artists.First(a => a.Name.Equals(name)).Name;
        }

        public List<Artist> GetRange(int startIndex, int numberOfItems) {
            return _context.Artists.Skip(startIndex).Take(numberOfItems).ToList();
        }
    }
}