using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.DAL.Repositories {
    public class TrackRepository : Repository<Track, string>, ITrackRepository {
        public TrackRepository(ApplicationContext context) : base(context) {
        }

        public async Task<Track> GetByName(string name) {
            return await _context.Tracks.FirstOrDefaultAsync(t => t.Name.Equals(name));
        }
    }
}