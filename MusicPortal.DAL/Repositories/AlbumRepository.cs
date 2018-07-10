using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories {
    public class AlbumRepository : Repository<Album, string>, IAlbumRepository {
        public AlbumRepository(ApplicationContext context) : base(context) {
        }

        public async Task<Album> GetByName(string name) {
            return await _context.Albums.FirstOrDefaultAsync(a => a.Name.Equals(name));
        }
    }
}
