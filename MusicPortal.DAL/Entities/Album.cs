using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPortal.DAL.Entities {
    public class Album {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AlbumId { get; set; }

        public string Name { get; set; }

        public string PictureURL { get; set; }

        public string ArtistId { get; set; }

        public Artist Artist { get; set; }

        public List<Track> Tracks { get; set; }
    }
}