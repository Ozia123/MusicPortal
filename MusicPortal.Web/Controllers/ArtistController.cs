﻿using System;
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
        [Route("api/chart/artists/{id}")]
        public async Task<IActionResult> GetTopArtists([Required]string id) {
            List<ArtistDto> artists = await _artistService.GetTopArtists(Convert.ToInt32(id), 20);
            if (artists == null) {
                return BadRequest("last.fm not responding");
            }

            return Ok(_mapper.Map<List<ArtistDto>, List<ArtistModel>>(artists));
        }
    }
}
