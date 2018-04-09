using System.Collections.Generic;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces {
    public interface ITrackRepository : IRepository<Track, string> {
        IEnumerable<Track> AddRange(IEnumerable<Track> items);
        List<Track> GetAll();
        Track GetByName(string name);
        List<Track> GetRange(int startIndex, int numberOfItems);
    }
}