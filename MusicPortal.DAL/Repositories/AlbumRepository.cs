using System.Linq;
using System.Collections.Generic;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    public class AlbumRepository : Repository<Album, string>, IAlbumRepository {
        public AlbumRepository(ApplicationContext context) : base(context) {
        }

        public IEnumerable<Album> AddRange(IEnumerable<Album> items) {
            List<Album> albums = new List<Album>();

            foreach (var item in items) {
                // TODO: Так будет медленно работать, почитай про AddRange
                albums.Add(_context.Albums.Add(item).Entity);
                // TODO: При наличии UnitOfWork, а SaveChanges вызывают через него, а не на каждую команду репозитория https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
                _context.SaveChanges();
            }
            return albums;
        }

        public List<Album> GetAll() {
            // TODO: тут нужно использовать методы базового репозитория
            return _context.Albums.ToList();
        }

        public Album GetByName(string name) {
            // TODO: тут нужно использовать методы базового репозитория
            return _context.Albums.FirstOrDefault(a => a.Name.Equals(name));
        }

        public List<Album> GetRange(int startIndex, int numberOfItems) {
            // TODO: тут нужно использовать методы базового репозитория
            return _context.Albums.Skip(startIndex).Take(numberOfItems).ToList();
        }
    }
}