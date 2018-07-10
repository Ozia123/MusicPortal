using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPortal.DAL.Entities {
    public class Artist {
        public Artist() {
            Albums = new HashSet<Album>();
            Tracks = new HashSet<Track>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ArtistId { get; set; }

        public string Name { get; set; }

        public string PictureURL { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Biography { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}