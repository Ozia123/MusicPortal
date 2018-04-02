using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPortal.DAL.Entities {
    public class Song {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SondId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SongURL { get; set; }

        public string AlbumId { get; set; }
        public Album Album { get; set; }
    }
}