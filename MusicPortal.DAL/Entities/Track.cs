﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPortal.DAL.Entities {
    public class Track {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TrackId { get; set; }
        
        public string ArtistId { get; set; }

        public string AlbumId { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }
        
        public string PictureURL { get; set; }

        public string TrackURL { get; set; }

        public string CloudURL { get; set; }
        
        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }
    }
}