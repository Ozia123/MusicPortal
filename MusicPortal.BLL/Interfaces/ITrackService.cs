using MusicPortal.BLL.Base;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces {
    public interface ITrackService : IService<TrackViewModel, string> {
        IQueryable<Track> Query();

        List<TrackViewModel> GetAlbumTracks(string albumName);

        Task<List<TrackViewModel>> GetTopTracks(int page, int itemsPerPage);

        Task<List<TrackViewModel>> GetTopArtistsTracks(string artistName, int page, int itemsPerPage);

        Task<TrackViewModel> UploadTrackThroughConsole(TrackViewModel track);
    }
}