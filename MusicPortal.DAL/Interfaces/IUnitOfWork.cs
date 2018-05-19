using MusicPortal.DAL.EF;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces {
    public interface IUnitOfWork {
        ApplicationContext Context { get; }

        IArtistRepository ArtistRepository { get; }
        IAlbumRepository AlbumRepository { get; }
        ITrackRepository TrackRepository { get; }

        Task SaveAsync();
    }
}
