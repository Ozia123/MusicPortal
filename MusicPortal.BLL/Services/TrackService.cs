﻿using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.BusinessModels;
using MusicPortal.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.BLL.Services {
    public class TrackService : ITrackService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly LastFm _lastFm;

        public TrackService(IUnitOfWork unitOfWork, IMapper mapper, LastFm lastFm) {
            _database = unitOfWork;
            _mapper = mapper;
            _lastFm = lastFm;
        }

        public IQueryable<Track> Query() {
            return _database.TrackRepository.Query();
        }

        public async Task<TrackDto> GetById(string id) {
            Track track = await _database.TrackRepository.GetById(id);
            return _mapper.Map<Track, TrackDto>(track);
        }

        public async Task<TrackDto> Create(TrackDto item) {
            Track track = _mapper.Map<TrackDto, Track>(item);
            track = await _database.TrackRepository.Create(track);
            return _mapper.Map<Track, TrackDto>(track);
        }

        public async Task<TrackDto> Update(TrackDto item) {
            Track track = _mapper.Map<TrackDto, Track>(item);
            track = await _database.TrackRepository.Update(track);
            return _mapper.Map<Track, TrackDto>(track);
        }

        public async Task<TrackDto> Delete(string id) {
            Track track = await _database.TrackRepository.Delete(id);
            return _mapper.Map<Track, TrackDto>(track);
        }

        public async Task<List<TrackDto>> GetTopTracks(int page, int itemsPerPage) {
            List<TrackDto> tracks = await _lastFm.GetTopTracks(page, itemsPerPage);
            return tracks;
        }
    }
}