using MusicPortal.ViewModels.Base;

namespace MusicPortal.ViewModels.ViewModels {
    public class TrackViewModel : IViewModel {
        public string TrackId { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }

        public int PlayCount { get; set; }

        public string PictureURL { get; set; }

        public string TrackURL { get; set; }

        public string CloudURL { get; set; }

        public string ArtistName { get; set; }

        public string AlbumName { get; set; }

        public string AlbumId { get; set; }

        public string ArtistId { get; set; }
    }
}
