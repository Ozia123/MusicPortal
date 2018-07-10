using System.Threading.Tasks;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces {
    public interface IAlbumRepository : IRepository<Album, string> {
        Task<Album> GetByName(string name);
    }
}