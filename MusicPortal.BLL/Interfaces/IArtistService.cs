using System.Collections.Generic;
using System.Linq;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;

namespace MusicPortal.BLL.Interfaces {
    public interface IArtistService : IService<ArtistDto, string> {
        IQueryable<Artist> Query();

        List<ArtistDto> GetTopArtists(int page, int itemsPerPage);
    }
}