using System.Collections.Generic;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces {
    public interface ISongRepository : IRepository<Song, string> {
        IEnumerable<Song> AddRange(IEnumerable<Song> items);
        List<Song> GetAll();
        List<Song> GetRange(int startIndex, int numberOfItems);
    }
}