using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using System.Linq;

namespace MusicPortal.BLL.Interfaces {
    public interface IAlbumService : IService<AlbumDto, string> {
        IQueryable<Album> Query();


    }
}