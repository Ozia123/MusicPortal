using MusicPortal.BLL.Base.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.BLL.Interfaces {
    public interface IArtistService : IService<ArtistViewModel, Artist, string> {
        Task<ArtistViewModel> GetByName(string name);

        Task<List<ArtistViewModel>> GetTopArtists(int page, int itemsPerPage);

        Task<List<ArtistViewModel>> GetSimilarArtists(string name);

        Task<Artist> GetArtistFromDatabaseOrIfNotFoundFromLastFmByName(string name);
    }
}