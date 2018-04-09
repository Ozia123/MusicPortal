using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces {
    public interface ITrackService : IService<TrackDto, string> {
        IQueryable<Track> Query();

        List<TrackDto> GetAlbumTracks(string albumName);
        Task<List<TrackDto>> GetTopTracks(int page, int itemsPerPage);
        Task<List<TrackDto>> GetTopArtistsTracks(string artistName, int page, int itemsPerPage);
    }
}