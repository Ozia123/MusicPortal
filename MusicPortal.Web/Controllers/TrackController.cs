using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MusicPortal.Web.Controllers {
    public class TrackController : Controller {
        private readonly ITrackService _trackService;

        public TrackController(ITrackService trackService) {
            _trackService = trackService;
        }

        [HttpGet]
        [Route("api/chart/tracks/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopTracks([Required]int page, [Required]int itemsPerPage) {
            List<TrackViewModel> tracks = await _trackService.GetTopTracks(page, itemsPerPage);
            if (tracks == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(tracks);
        }

        [HttpGet]
        [Route("api/artist/top-tracks/{artistName}/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopArtistsTracks([Required]string artistName, [Required]int page, [Required]int itemsPerPage) {
            List<TrackViewModel> tracks = await _trackService.GetTopArtistsTracks(artistName, page, itemsPerPage);
            if (tracks == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(tracks);
        }

        [HttpGet]
        [Route("api/album-tracks/{albumName}")]
        public IActionResult GetAlbumTracks([Required]string albumName) {
            List<TrackViewModel> tracks = _trackService.GetAlbumTracks(albumName);
            if (tracks == null) {
                return BadRequest("tracks not found in database");
            }

            return Ok(tracks);
        }

        [HttpPost]
        [Route("api/track/update")]
        public async Task<IActionResult> SetTrackCloudUrl([FromBody]TrackViewModel trackModel) {
            TrackViewModel track = await _trackService.Update(trackModel);
            if (track == null) {
                return BadRequest("trackId is incorrect");
            }

            return Ok(track);
        }

        [HttpPost]
        [Route("api/track/console-upload")]
        public async Task<IActionResult> UploadTrackThroughConsole([FromBody]TrackViewModel trackModel) {
            TrackViewModel track = await _trackService.UploadTrackThroughConsole(trackModel);
            if (track == null) {
                return BadRequest("some data is incorrect");
            }

            return Ok(track);
        }
    }
}
