using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPortal.DAL.Entities {
    public class Album {
        public Album() {
            Tracks = new HashSet<Track>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AlbumId { get; set; }
        
        [ForeignKey("Artists")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ArtistId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string PictureURL { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}