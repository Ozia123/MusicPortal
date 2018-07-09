using MusicPortal.BLL.Base;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces {
    public interface IAlbumService : IService<AlbumViewModel, string> {
        IQueryable<Album> Query();

        Task<AlbumViewModel> GetByName(string name);

        Task<List<AlbumViewModel>> GetTopArtistsAlbums(string name, int page, int itemsPerPage);
    }
}