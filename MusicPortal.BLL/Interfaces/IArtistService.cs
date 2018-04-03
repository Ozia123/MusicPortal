using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;

namespace MusicPortal.BLL.Interfaces {
    public interface IArtistService : IService<TrackDto, string> {
        IQueryable<Track> Query();

        Task<List<TrackDto>> GetTopArtists(int page, int itemsPerPage);
    }
}