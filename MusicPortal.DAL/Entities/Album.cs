using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPortal.DAL.Entities {
    public class Album {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // TODO: Лучше использовать GUID или long
        public string AlbumId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string PictureURL { get; set; }

        // TODO: Лучше использовать GUID или long
        public string ArtistId { get; set; }

        public Artist Artist { get; set; }

        public List<Track> Tracks { get; set; }
    }
}