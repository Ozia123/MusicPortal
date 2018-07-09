import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AlbumService } from '../../services/album/album.service';
import { AlbumModel } from '../../models/AlbumModel';
import { TrackModel } from '../../models/TrackModel';
import { TrackService } from '../../services/track/track.service';

import { TrackComponent } from '../track/track.component';

@Component({
  selector: 'app-album-page',
  templateUrl: './album-page.component.html',
  styleUrls: ['./album-page.component.css'],
  providers: [AlbumService, TrackService]
})
export class AlbumPageComponent implements OnInit {
  album: AlbumModel = null;
  tracks: TrackModel[] = null;

  constructor(
    private router: Router, 
    private route: ActivatedRoute, 
    private albumService: AlbumService,
    private trackService: TrackService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.Init();
    });
  }

  private async Init() {
    let albumName: string = this.getAlbumName();
    await this.setAlbum(albumName);
    await this.setTracks(albumName);
  }

  private getAlbumName(): string {
    let albumName: string = this.route.snapshot.paramMap.get('name') || '';
    if (albumName === '') {
      this.router.navigate(['']);
    }
    return albumName;
  }

  private async setAlbum(albumName: string) {
    this.album = await this.albumService.getFullInfoAlbum(albumName);
    if (this.album == null) {
      this.router.navigate(['']);
    }
  }

  private async setTracks(albumName: string) {
    let tracks: TrackModel[] = await this.trackService.getAlbumTracks(albumName);
    if (tracks == null) {
      this.router.navigate(['']);
    }
    this.tracks = this.getFullInfoTracksArray(tracks);
  }

  private getFullInfoTracksArray(tracks: TrackModel[]): TrackModel[] {
    tracks.forEach(track => {
      track.pictureURL = this.album.pictureURL;
      track.artistName = this.album.artistName;
    });
    return tracks;
  }
}
