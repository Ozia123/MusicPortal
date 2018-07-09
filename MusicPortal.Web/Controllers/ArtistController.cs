using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MusicPortal.Web.Controllers {
    public class ArtistController : Controller {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService) {
            _artistService = artistService;
        }

        [HttpGet]
        [Route("api/chart/artists/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopArtists([Required]int page, [Required]int itemsPerPage) {
            List<ArtistViewModel> artists = await _artistService.GetTopArtists(page, itemsPerPage);
            if (artists == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(artists);
        }

        [HttpGet]
        [Route("api/artist/{name}")]
        public async Task<IActionResult> GetFullInfoArtist([Required]string name) {
            ArtistViewModel artist = await _artistService.GetByName(name);
            if (artist == null) {
                return BadRequest("artist not found");
            }

            return Ok(artist);
        }

        [HttpGet]
        [Route("api/similar-artists/{name}")]
        public async Task<IActionResult> GetSimilarArtists([Required]string name) {
            List<ArtistViewModel> artists = await _artistService.GetSimilarArtists(name);
            if (artists == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(artists);
        }
    }
}
