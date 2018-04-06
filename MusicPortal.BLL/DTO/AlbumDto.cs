using System;

namespace MusicPortal.BLL.DTO {
    public class AlbumDto {
        public string AlbumId { get; set; }

        public string Name { get; set; }

        public int PlayCount { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string PictureURL { get; set; }

        public string ArtistName { get; set; }

        public string ArtistId { get; set; }
    }
}