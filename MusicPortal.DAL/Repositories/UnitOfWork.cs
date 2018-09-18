using System.Threading.Tasks;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories {
    public class UnitOfWork : IUnitOfWork {
        public UnitOfWork(ApplicationContext context) {
            Context = context;
            ArtistRepository = new ArtistRepository(context);
            AlbumRepository = new AlbumRepository(context);
            TrackRepository = new TrackRepository(context);
        }

        public ApplicationContext Context { get; private set; }

        public IArtistRepository ArtistRepository { get; private set; } 

        public IAlbumRepository AlbumRepository { get; private set; }

        public ITrackRepository TrackRepository { get; private set; }

        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class {
            return new Repository<TEntity, TKey>(Context);
        } 

        public void Save() {
            Context.SaveChanges();
        }

        public async Task SaveAsync() {
            await Context.SaveChangesAsync();
        }
    }
}