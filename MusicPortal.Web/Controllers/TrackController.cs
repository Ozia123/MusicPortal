using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.Web.Models;

namespace MusicPortal.Web.Controllers {
    public class TrackController : Controller {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public TrackController(
            ITrackService trackService,
            IMapper mapper
        ) {
            _trackService = trackService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/chart/tracks/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopTracks([Required]int page, [Required]int itemsPerPage) {
            List<TrackDto> tracks = await _trackService.GetTopTracks(page, itemsPerPage);
            if (tracks == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(_mapper.Map<List<TrackDto>, List<TrackModel>>(tracks));
        }

        [HttpGet]
        [Route("api/artist/top-tracks/{artistName}/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopArtistsTracks([Required]string artistName, [Required]int page, [Required]int itemsPerPage) {
            List<TrackDto> tracks = await _trackService.GetTopArtistsTracks(artistName, page, itemsPerPage);
            if (tracks == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(_mapper.Map<List<TrackDto>, List<TrackModel>>(tracks));
        }

        [HttpGet]
        [Route("api/album-tracks/{albumName}")]
        public IActionResult GetAlbumTracks([Required]string albumName) {
            List<TrackDto> tracks = _trackService.GetAlbumTracks(albumName);
            if (tracks == null) {
                return BadRequest("tracks not found in database");
            }

            return Ok(_mapper.Map<List<TrackDto>, List<TrackModel>>(tracks));
        }

        [HttpPost]
        [Route("api/track/update")]
        public async Task<IActionResult> SetTrackCloudUrl([FromBody]TrackModel trackModel) {
            TrackDto track = await _trackService.Update(_mapper.Map<TrackModel, TrackDto>(trackModel));
            if (track == null) {
                return BadRequest("trackId is incorrect");
            }

            return Ok(_mapper.Map<TrackDto, TrackModel>(track));
        }

        [HttpPost]
        [Route("api/track/console-upload")]
        public async Task<IActionResult> UploadTrackThroughConsole([FromBody]TrackModel trackModel) {
            TrackDto track = await _trackService.UploadTrackThroughConsole(_mapper.Map<TrackModel, TrackDto>(trackModel));
            if (track == null) {
                return BadRequest("some data is incorrect");
            }

            return Ok(_mapper.Map<TrackDto, TrackModel>(track));
        }
    }
}
