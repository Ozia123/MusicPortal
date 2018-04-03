namespace MusicPortal.Web.Models {
    public class TrackModel {
        public string TrackId { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }

        public int PlayCount { get; set; }

        public string PictureURL { get; set; }

        public string TrackURL { get; set; }

        public string AlbumId { get; set; }
    }
}