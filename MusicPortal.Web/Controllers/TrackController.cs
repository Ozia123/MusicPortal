using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MusicPortal.Web.Controllers {
    public class TrackController : Controller {
        private readonly ITrackService trackService;

        public TrackController(ITrackService trackService) {
            this.trackService = trackService;
        }

        [HttpGet]
        [Route("api/chart/tracks/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopTracks([Required]int page, [Required]int itemsPerPage) {
            List<TrackViewModel> tracks = await trackService.GetTopTracks(page, itemsPerPage);
            if (tracks == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(tracks);
        }

        [HttpGet]
        [Route("api/chart/pagination-tracks-count")]
        public async Task<IActionResult> GetCountOfTracksForPagination() {
            var tracksCount = await trackService.Query().CountAsync();
            return Ok(new { Count = tracksCount });
        }

        [HttpGet]
        [Route("api/artist/top-tracks/{artistName}/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopArtistsTracks([Required]string artistName, [Required]int page, [Required]int itemsPerPage) {
            List<TrackViewModel> tracks = await trackService.GetTopArtistsTracks(artistName, page, itemsPerPage);
            if (tracks == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(tracks);
        }

        [HttpGet]
        [Route("api/album-tracks/{albumName}")]
        public IActionResult GetAlbumTracks([Required]string albumName) {
            List<TrackViewModel> tracks = trackService.GetAlbumTracks(albumName);
            if (tracks == null) {
                return BadRequest("tracks not found in database");
            }

            return Ok(tracks);
        }

        [HttpPost]
        [Route("api/track/update")]
        public async Task<IActionResult> SetTrackCloudUrl([FromBody]TrackViewModel trackModel) {
            TrackViewModel track = await trackService.Update(trackModel);
            if (track == null) {
                return BadRequest("trackId is incorrect");
            }

            return Ok(track);
        }

        [HttpPost]
        [Route("api/track/console-upload")]
        public async Task<IActionResult> UploadTrackThroughConsole([FromBody]TrackViewModel trackModel) {
            TrackViewModel track = await trackService.UploadTrackThroughConsole(trackModel);
            if (track == null) {
                return BadRequest("some data is incorrect");
            }

            return Ok(track);
        }
    }
}
