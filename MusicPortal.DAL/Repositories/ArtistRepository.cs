using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories {
    class ArtistRepository : Repository<Artist, string>, IArtistRepository {
        public ArtistRepository(ApplicationContext context) : base(context) {
        }

        public async Task<string> GetIdByName(string name) {
            return (await _context.Artists.FirstAsync(a => a.Name.Equals(name))).ArtistId;
        }

        public async Task<Artist> GetByName(string name) {
            return await _context.Artists.FirstOrDefaultAsync(a => a.Name.Equals(name));
        }
    }
}