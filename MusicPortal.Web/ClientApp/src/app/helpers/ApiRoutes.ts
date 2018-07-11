export class ApiRoutes {
    public static getTopArtists = 'api/chart/artists/';
    public static getFullInfoArtist = 'api/artist/';
    public static getSimilarArtists = 'api/similar-artists/';
    public static getCountOfArtists = 'api/chart/pagination-artists-count';

    public static getFullInfoAlbum = 'api/album/';
    public static getTopArtistsAlbums = 'api/artist-albums/';

    public static getAlbumTracks = 'api/album-tracks/';
    public static getTopArtistsTracks = 'api/artist/top-tracks/';
    public static getTopTracks = 'api/chart/tracks/';
    public static updateTrack = 'api/track/update';
    public static getCountOfTracks = 'api/chart/pagination-tracks-count';

    public static signUp = 'api/accounts';
    public static login = 'api/auth';
}
