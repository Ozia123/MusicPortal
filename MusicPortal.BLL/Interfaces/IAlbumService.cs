using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces {
    public interface IAlbumService : IService<AlbumDto, string> {
        IQueryable<Album> Query();

        Task<AlbumDto> GetByName(string name);
        Task<List<AlbumDto>> GetTopArtistsAlbums(string name, int page, int itemsPerPage);
    }
}