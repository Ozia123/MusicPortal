using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;

namespace MusicPortal.BLL.Interfaces {
    public interface IArtistService : IService<ArtistDto, string> {
        IQueryable<Artist> Query();

        ArtistDto GetByName(string name);
        Task<List<ArtistDto>> GetTopArtists(int page, int itemsPerPage);
        Task<List<ArtistDto>> GetSimilarArtists(string name);
        List<ArtistDto> GetArtistWhichPictureURLContainsThreeA();
    }
}