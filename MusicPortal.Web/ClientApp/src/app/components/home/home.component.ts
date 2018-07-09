import { ArtistModel } from '../../models/ArtistModel';
import { TrackModel } from '../../models/TrackModel';
import { Component } from '@angular/core';
import { PageEvent, MatPaginator } from '@angular/material';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { ArtistService } from '../../services/artist/artist.service';
import { TrackService } from '../../services/track/track.service';

import { ArtistComponent } from '../artist/artist.component';
import { TrackComponent } from '../track/track.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [ArtistService, TrackService],
})
export class HomeComponent {
  artists: ArtistModel[] = null;
  tracks: TrackModel[] = null;

  constructor(private router: Router, private artistService: ArtistService, private trackService: TrackService) {
    this.Init();
  }

  async Init() {
    this.artists = await this.artistService.getTopArtists(1, 6);
    this.tracks = await this.trackService.getTopTracks(1, 10);
  }
}