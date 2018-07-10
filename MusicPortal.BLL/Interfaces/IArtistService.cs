using MusicPortal.BLL.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.BLL.Interfaces {
    public interface IArtistService : IService<ArtistViewModel, string> {
        IQueryable<Artist> Query();

        Task<ArtistViewModel> GetByName(string name);

        Task<List<ArtistViewModel>> GetTopArtists(int page, int itemsPerPage);

        Task<List<ArtistViewModel>> GetSimilarArtists(string name);

        Task<Artist> GetArtistFromDatabaseOrIfNotFoundFromLastFmByName(string name);
    }
}