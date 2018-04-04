import { ArtistModel } from '../models/ArtistModel';
import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { ArtistService } from '../services/artist.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [ArtistService],
})
export class HomeComponent {
  artists: ArtistModel[] = null;

  constructor(private router: Router, private artistService: ArtistService) {
    this.Init();
  }

  async Init() {
    this.artists = await this.artistService.getTopArtists(1, 20);
  }
}