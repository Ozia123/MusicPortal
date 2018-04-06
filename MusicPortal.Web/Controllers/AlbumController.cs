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
    public class AlbumController : Controller {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        public AlbumController(
            IAlbumService albumService,
            IMapper mapper
        ) {
            _albumService = albumService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/artist-albums/{name}/{page}")]
        public async Task<IActionResult> GetTopArtists([Required]string name, [Required]int page) {
            List<AlbumDto> albums = await _albumService.GetTopArtistsAlbums(name, page, 10);
            if (albums == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(_mapper.Map<List<AlbumDto>, List<AlbumModel>>(albums));
        }
    }
}
