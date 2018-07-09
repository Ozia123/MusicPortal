using MusicPortal.ViewModels.Base;

namespace MusicPortal.ViewModels.ViewModels {
    public class ArtistViewModel : IViewModel {
        public string ArtistId { get; set; }

        public string Name { get; set; }

        public string PictureURL { get; set; }

        public string Biography { get; set; }
    }
}
