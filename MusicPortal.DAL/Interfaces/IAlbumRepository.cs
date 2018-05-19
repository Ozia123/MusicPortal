using System.Collections.Generic;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces {
    public interface IAlbumRepository : IRepository<Album, string> {
        IEnumerable<Album> AddRange(IEnumerable<Album> items);
        List<Album> GetAll();
        Album GetByName(string name);
        List<Album> GetRange(int startIndex, int numberOfItems);
    }
}