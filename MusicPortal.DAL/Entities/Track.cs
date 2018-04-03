using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPortal.DAL.Entities {
    public class Track {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TrackId { get; set; }

        public string Name { get; set; }
        
        public string PictureURL { get; set; }

        public string TrackURL { get; set; }

        public string AlbumId { get; set; }

        public Album Album { get; set; }
    }
}