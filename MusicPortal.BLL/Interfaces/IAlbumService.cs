using MusicPortal.BLL.Base.Abstraction;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces {
    public interface IAlbumService : IService<AlbumViewModel, Album, string> {
        Task<AlbumViewModel> GetByName(string name);

        Task<List<AlbumViewModel>> GetTopArtistsAlbums(string name, int page, int itemsPerPage);
    }
}