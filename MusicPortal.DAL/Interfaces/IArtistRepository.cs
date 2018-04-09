using System.Collections.Generic;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces {
    public interface IArtistRepository : IRepository<Artist, string> {
        IEnumerable<Artist> AddRange(List<Artist> items);
        List<Artist> GetAll();
        Artist GetByName(string name);
        string GetIdByName(string name);
        List<Artist> GetRange(int startIndex, int numberOfItems);
    }
}