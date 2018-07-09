import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../../services/artist/artist.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ArtistModel } from '../../models/ArtistModel';
import { AlbumModel } from '../../models/AlbumModel';
import { AlbumService } from '../../services/album/album.service';
import { TrackService } from '../../services/track/track.service';
import { TrackModel } from '../../models/TrackModel';

import { AlbumComponent } from '../album/album.component';
import { TrackComponent } from '../track/track.component';

@Component({
  selector: 'app-artist-profile',
  templateUrl: './artist-profile.component.html',
  styleUrls: ['./artist-profile.component.css'],
  providers: [ArtistService, AlbumService, TrackService]
})
export class ArtistProfileComponent implements OnInit {
  private albumsPage: number = 1;
  private tracksPage: number = 1;
  private albumsPerPage: number = 10;
  private tracksPerPage: number = 20;

  private isMoreAlbums: boolean = false;
  private isMoreTracks: boolean = false;

  showAlbumsPageSpinner: boolean = true;
  showTracksPageSpinner: boolean = true;

  artist: ArtistModel = null;
  albums: AlbumModel[] = null;
  tracks: TrackModel[] = null;
  similarArtists: ArtistModel[] = null;

  constructor(
    private router: Router, 
    private route: ActivatedRoute, 
    private artistService: ArtistService,
    private albumService: AlbumService,
    private trackService: TrackService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.similarArtists = null;
      this.Init();
    });
  }

  private async Init() {
    let artistName: string = this.route.snapshot.paramMap.get('name') || '';
    if (artistName === '') {
      this.router.navigate(['']);
    }
    await this.setArtist(artistName);
    await this.setAlbums();
    await this.setTracks();
    await this.setSimilarArtists();
  }

  private async setArtist(name: string) {
    this.similarArtists = null;
    this.tracks = null;
    this.albums = null;
    this.artist = await this.getArtist(name);
    if (this.artist == null) {
      this.router.navigate(['']);
    }
  }

  private async getArtist(name: string): Promise<ArtistModel> {
    return await this.artistService.getFullInfoArtist(name);
  }

  private async setAlbums() {
    this.albums = await this.getTopAlbums(1);
    this.isMoreAlbums = this.isMoreAlbumsAvailable(null);
  }

  private async getTopAlbums(pageIndex: number): Promise<AlbumModel[]> {
    this.showAlbumsPageSpinner = true;
    this.isMoreAlbums = false;
    let albums: AlbumModel[] = await this.albumService.getTopArtistsAlbums(this.artist.name, pageIndex, this.albumsPerPage);
    this.showAlbumsPageSpinner = false;
    return albums;
  }

  private async setTracks() {
    this.tracks = await this.getTopTracks(this.artist.name, 1);
    this.isMoreTracks = this.isMoreTracksAvailable(null);
  }

  private async getTopTracks(artistName: string, pageIndex: number): Promise<TrackModel[]> {
    this.showTracksPageSpinner = true;
    this.isMoreTracks = false;
    let tracks: TrackModel[] = await this.trackService.getTopArtistsTracks(artistName, pageIndex, this.tracksPerPage);
    this.showTracksPageSpinner = false;
    return tracks;
  }

  private async setSimilarArtists() {
    this.similarArtists = await this.artistService.getSimilarArtists(this.artist.name);
  }

  async onMoreAlbums() {
    let newAlbums: AlbumModel[] = await this.getTopAlbums(++this.albumsPage);
    this.isMoreAlbums = this.isMoreAlbumsAvailable(newAlbums);
    if (this.isMoreAlbums) {
      this.isMoreAlbums = this.checkIfItsTheSameAlbumsFromTheServer(newAlbums);
      if (!this.isMoreAlbums) {
        return;
      }
    }
    this.albums = this.albums.concat(newAlbums);
  }

  async onMoreTracks() {
    let newTracks: TrackModel[] = await this.getTopTracks(this.artist.name, ++this.tracksPage);
    this.isMoreTracks = this.isMoreTracksAvailable(newTracks);
    if (this.isMoreTracks) {
      this.isMoreTracks = this.checkIfItsTheSameTracksFromTheServer(newTracks);
      if (!this.isMoreTracks) {
        return;
      }
    }
    this.tracks = this.tracks.concat(newTracks);
  }

  isMoreAlbumsAvailable(newAlbums: AlbumModel[]): boolean {
    if (newAlbums == null) {
      return this.albums.length == this.albumsPerPage;
    }
    return newAlbums.length == this.albumsPerPage;
  }

  checkIfItsTheSameAlbumsFromTheServer(newAlbums: AlbumModel[]): boolean {
    let indexOfPreviousResponse: number = this.albums.length - this.albumsPerPage;
    return newAlbums[0].name != this.albums[indexOfPreviousResponse].name;
  }

  isMoreTracksAvailable(newTracks: TrackModel[]): boolean {
    if (newTracks == null) {
      return this.tracks.length == this.tracksPerPage;
    }
    return newTracks.length == this.tracksPerPage;
  }

  checkIfItsTheSameTracksFromTheServer(newTracks: TrackModel[]): boolean {
    let indexOfPreviousResponse: number = this.tracks.length - this.tracksPerPage;
    return newTracks[0].name != this.tracks[indexOfPreviousResponse].name;
  }
}
