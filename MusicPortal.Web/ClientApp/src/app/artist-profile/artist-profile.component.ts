import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../services/artist.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ArtistModel } from '../models/ArtistModel';

@Component({
  selector: 'app-artist-profile',
  templateUrl: './artist-profile.component.html',
  styleUrls: ['./artist-profile.component.css'],
  providers: [ArtistService]
})
export class ArtistProfileComponent implements OnInit {
  artist: ArtistModel = null;

  constructor(private router: Router, private route: ActivatedRoute, private artistService: ArtistService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.Init();
    });
  }

  async Init() {
    let artistName: string = this.route.snapshot.paramMap.get('name') || '';
    if (artistName == '') {
      this.router.navigate(['']);
    }
    this.artist = await this.getArtist(artistName);
    if (this.artist == null) {
      this.router.navigate(['']);
    }
  }

  async getArtist(name: string): Promise<ArtistModel> {
    return await this.artistService.getFullInfoArtist(name);
  }
}