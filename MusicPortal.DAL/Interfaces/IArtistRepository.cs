using System.Collections.Generic;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces {
    public interface IArtistRepository : IRepository<Track, string> {
        IEnumerable<Track> AddRange(List<Track> items);
        List<Track> GetAll();
        List<Track> GetRange(int startIndex, int numberOfItems);
    }
}