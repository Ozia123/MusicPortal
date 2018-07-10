using System.Threading.Tasks;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces {
    public interface ITrackRepository : IRepository<Track, string> {
        Task<Track> GetByName(string name);
    }
}