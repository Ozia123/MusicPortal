using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MusicPortal.Web.Controllers {
    public class ArtistController : Controller {
        private readonly IArtistService artistService;

        public ArtistController(IArtistService artistService) {
            this.artistService = artistService;
        }

        [HttpGet]
        [Route("api/chart/artists/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopArtists([Required]int page, [Required]int itemsPerPage) {
            List<ArtistViewModel> artists = await artistService.GetTopArtists(page, itemsPerPage);
            if (artists == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(artists);
        }

        [HttpGet]
        [Route("api/chart/pagination-artists-count")]
        public async Task<IActionResult> GetCountOfArtistsForPagination() {
            var artistsCount = await artistService.Query().CountAsync();
            return Ok(new { Count = artistsCount });
        }

        [HttpGet]
        [Route("api/artist/{name}")]
        public async Task<IActionResult> GetFullInfoArtist([Required]string name) {
            ArtistViewModel artist = await artistService.GetByName(name);
            if (artist == null) {
                return BadRequest("artist not found");
            }

            return Ok(artist);
        }

        [HttpGet]
        [Route("api/similar-artists/{name}")]
        public async Task<IActionResult> GetSimilarArtists([Required]string name) {
            List<ArtistViewModel> artists = await artistService.GetSimilarArtists(name);
            if (artists == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(artists);
        }
    }
}
