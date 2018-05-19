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
        [Route("api/artist-albums/{name}/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopArtists([Required]string name, [Required]int page, [Required]int itemsPerPage) {
            List<AlbumDto> albums = await _albumService.GetTopArtistsAlbums(name, page, itemsPerPage);
            if (albums == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(_mapper.Map<List<AlbumDto>, List<AlbumModel>>(albums));
        }

        [HttpGet]
        [Route("api/album/{name}")]
        public async Task<IActionResult> GetFullInfoAlbum([Required]string name) {
            AlbumDto album = await _albumService.GetByName(name);
            if (album == null) {
                return BadRequest("no such album in database");
            }

            return Ok(_mapper.Map<AlbumDto, AlbumModel>(album));
        }
    }
}
