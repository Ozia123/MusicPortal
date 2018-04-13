using System.Linq;
using System.Collections.Generic;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    public class TrackRepository : Repository<Track, string>, ITrackRepository {
        public TrackRepository(ApplicationContext context) : base(context) {
        }

        // TODO: тут нужно использовать методы базового репозитория

        public IEnumerable<Track> AddRange(IEnumerable<Track> items) {
            List<Track> songs = new List<Track>();

            foreach (var item in items) {
                songs.Add(_context.Tracks.Add(item).Entity);
                _context.SaveChanges();
            }
            return songs;
        }

        public List<Track> GetAll() {
            return _context.Tracks.ToList();
        }

        public Track GetByName(string name) {
            return _context.Tracks.FirstOrDefault(t => t.Name.Equals(name));
        }

        public List<Track> GetRange(int startIndex, int numberOfItems) {
            return _context.Tracks.Skip(startIndex).Take(numberOfItems).ToList();
        }
    }
}