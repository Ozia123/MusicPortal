using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Interfaces;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.Web.Controllers {
    public class AlbumController : Controller {
        private readonly IAlbumService albumService;

        public AlbumController(IAlbumService albumService) {
            this.albumService = albumService;
        }

        [HttpGet]
        [Route("api/artist-albums/{name}/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopArtists([Required]string name, [Required]int page, [Required]int itemsPerPage) {
            List<AlbumViewModel> albums = await albumService.GetTopArtistsAlbums(name, page, itemsPerPage);
            if (albums == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(albums);
        }

        [HttpGet]
        [Route("api/album/{name}")]
        public async Task<IActionResult> GetFullInfoAlbum([Required]string name) {
            AlbumViewModel album = await albumService.GetByName(name);
            if (album == null) {
                return BadRequest("no such album in database");
            }

            return Ok(album);
        }
    }
}
