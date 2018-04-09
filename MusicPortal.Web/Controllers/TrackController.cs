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
    }
}
