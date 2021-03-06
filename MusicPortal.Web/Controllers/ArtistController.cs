﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.Web.Models;

namespace MusicPortal.Web.Controllers {
    public class ArtistController : Controller {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistController(
            IArtistService artistService,
            IMapper mapper
        ) {
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/chart/artists/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetTopArtists([Required]int page, [Required]int itemsPerPage) {
            List<ArtistDto> artists = await _artistService.GetTopArtists(page, itemsPerPage);
            if (artists == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(_mapper.Map<List<ArtistDto>, List<ArtistModel>>(artists));
        }

        [HttpGet]
        [Route("api/artist/{name}")]
        public IActionResult GetFullInfoArtist([Required]string name) {
            ArtistDto artist = _artistService.GetByName(name);
            if (artist == null) {
                return BadRequest("artist not found");
            }

            return Ok(_mapper.Map<ArtistDto, ArtistModel>(artist));
        }

        [HttpGet]
        [Route("api/similar-artists/{name}")]
        public async Task<IActionResult> GetSimilarArtists([Required]string name) {
            List<ArtistDto> artists = await _artistService.GetSimilarArtists(name);
            if (artists == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(_mapper.Map<List<ArtistDto>, List<ArtistModel>>(artists));
        }

        [HttpGet]
        [Route("api/artist/filtered")]
        public IActionResult GetArtistWhichPictureURLContainsThreeA() {
            List<ArtistDto> artists = _artistService.GetArtistWhichPictureURLContainsThreeA();
            if (artists == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(_mapper.Map<List<ArtistDto>, List<ArtistModel>>(artists));
        }
    }
}
