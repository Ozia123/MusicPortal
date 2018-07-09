using MusicPortal.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace MusicPortal.ViewModels.ViewModels {
    public class AlbumViewModel : IViewModel {
        public string AlbumId { get; set; }

        public string Name { get; set; }

        public int PlayCount { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string PictureURL { get; set; }

        public string ArtistName { get; set; }

        public string ArtistId { get; set; }

        public List<string> TrackNames { get; set; }
    }
}
