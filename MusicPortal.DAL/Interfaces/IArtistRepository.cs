using MusicPortal.DAL.Entities;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces {
    public interface IArtistRepository : IRepository<Artist, string> {
        Task<string> GetIdByName(string name);

        Task<Artist> GetByName(string name);
    }
}