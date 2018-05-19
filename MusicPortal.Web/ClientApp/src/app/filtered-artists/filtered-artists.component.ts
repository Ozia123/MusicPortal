import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../services/artist.service';
import { ArtistModel } from '../models/ArtistModel';

@Component({
  selector: 'app-filtered-artists',
  templateUrl: './filtered-artists.component.html',
  styleUrls: ['./filtered-artists.component.css']
})
export class FilteredArtistsComponent implements OnInit {
  filteedArtists: ArtistModel[];

  constructor(private artistService: ArtistService) { }

  ngOnInit() {
    this.Init();
  }

  async Init() {
    this.filteedArtists = await this.artistService.getFilteredArtists();
  }

}
