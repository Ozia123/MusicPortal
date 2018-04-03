namespace MusicPortal.BLL.Interfaces {
    interface IServiceCreator {
        IArtistService CreateArtistService();
        IAlbumService CreateAlbumService();
        ITrackService CreateTrackService();
    }
}