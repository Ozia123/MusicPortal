using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPortal.Facade.Interfaces {
    public interface IMusicPortalClient {
        Task<ArtistViewModel> GetFullInfoArtist(string artistName);

        Task<AlbumViewModel> GetFullInfoAlbum(string artistName, string albumName);

        Task<TrackViewModel> GetFullInfoTrack(string artistName, string trackName);

        Task<List<ArtistViewModel>> GetTopArtists(int page = 1, int itemsPerPage = 20);

        Task<List<ArtistViewModel>> GetSimilarArtists(string name);

        Task<List<TrackViewModel>> GetTopTracks(int page = 1, int itemsPerPage = 20);

        Task<List<AlbumViewModel>> GetTopArtistsAlbums(string name, int page = 1, int itemsPerPage = 20);

        Task<List<TrackViewModel>> GetTopArtistsTracks(string name, int page = 1, int itemsPerPage = 20);
    }
}
