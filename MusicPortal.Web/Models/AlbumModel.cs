using System;

namespace MusicPortal.Web.Models {
    public class AlbumModel {
        public string AlbumId { get; set; }

        public string Name { get; set; }

        public int PlayCount { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string PictureURL { get; set; }

        public string ArtistName { get; set; }

        public string ArtistId { get; set; }
    }
}