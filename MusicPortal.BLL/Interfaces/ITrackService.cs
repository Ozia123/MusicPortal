using MusicPortal.BLL.Base.Abstraction;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces {
    public interface ITrackService : IService<TrackViewModel, Track, string> {
        Task<List<TrackViewModel>> GetAlbumTracks(string albumName);

        Task<List<TrackViewModel>> GetTopTracks(int page, int itemsPerPage);

        Task<List<TrackViewModel>> GetTopArtistsTracks(string artistName, int page, int itemsPerPage);

        Task<TrackViewModel> UploadTrackThroughConsole(TrackViewModel track);
    }
}