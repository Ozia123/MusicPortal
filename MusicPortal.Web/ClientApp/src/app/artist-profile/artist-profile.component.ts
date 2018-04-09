import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../services/artist.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ArtistModel } from '../models/ArtistModel';
import { AlbumModel } from '../models/AlbumModel';
import { AlbumService } from '../services/album.service';
import { TrackService } from '../services/track.service';
import { TrackModel } from '../models/TrackModel';

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
  }

  private async getTopAlbums(pageIndex: number): Promise<AlbumModel[]> {
    return await this.albumService.getTopArtistsAlbums(this.artist.name, pageIndex);
  }

  private async setTracks() {
    this.tracks = await this.getTopTracks(this.artist.name, 1);
  }

  private async getTopTracks(artistName: string, pageIndex: number): Promise<TrackModel[]> {
    return await this.trackService.getTopArtistsTracks(artistName, pageIndex);
  }

  private async setSimilarArtists() {
    this.similarArtists = await this.artistService.getSimilarArtists(this.artist.name);
  }

  async onMoreAlbums() {
    let newAlbums: AlbumModel[] = await this.getTopAlbums(++this.albumsPage);
    this.albums = this.albums.concat(newAlbums);
  }

  async onMoreTracks() {
    let newTracks: TrackModel[] = await this.getTopTracks(this.artist.name, ++this.tracksPage);
    this.tracks = this.tracks.concat(newTracks);
  }
}
