using IF.Lastfm.Core.Api;
using IF.Lastfm.Core.Api.Helpers;
using IF.Lastfm.Core.Objects;
using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.BusinessModels {
    public static class LastFmDtoMapper {
        public static TrackDto MapTrack(LastTrack track) {
            return new TrackDto {
                Name = track.Name,
                TrackURL = track.Url.AbsolutePath;
            };
        }

        public static List<TrackDto> MapTracks(PageResponse<LastTrack> tracks) {
            List<TrackDto> trackDtoList = new List<TrackDto>();
            foreach (var track in tracks) {
                TrackDto trackDto = MapTrack(track);
                trackDtoList.Add();
            }
        }
    }
}
