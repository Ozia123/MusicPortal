import { ArtistModel } from '../models/ArtistModel';
import { Component } from '@angular/core';
import { PageEvent, MatPaginator } from '@angular/material';
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
  artistsSet: Set<ArtistModel> = null;
  
  artistsLength: number = 100;
  artistsPageSize: number = 10;
  artistsPageSizeOptions: number[] = [10, 20, 30];

  constructor(private router: Router, private artistService: ArtistService) {
    this.Init();
  }

  async Init() {
    this.artists = await this.getArtistsFromHttp(1);
    this.artistsSet = new Set<ArtistModel>(this.artists);
  }

  async getArtistsFromHttp(pageIndex: number): Promise<ArtistModel[]> {
    return await this.artistService.getTopArtists(pageIndex, this.artistsPageSize);
  }

  getArtistsFromSet(pageIndex: number): ArtistModel[] {
    let startIndex: number = (pageIndex - 1) * this.artistsPageSize;
    let endIndex: number = pageIndex * this.artistsPageSize;
    let artistsArray: ArtistModel[] = [];
    for (let i = startIndex; i < endIndex; i++) {
      artistsArray.push(this.artistsSet[i]);
    }
    return artistsArray;
  }

  async getArtists(pageIndex: number): Promise<ArtistModel[]> {
    if (pageIndex * this.artistsPageSize > this.artistsSet.size) {
      this.artists = null;
      return await this.getArtistsFromHttp(pageIndex);
    }
    return this.getArtistsFromSet(pageIndex);
  }

  async changeArtistsPage(event: PageEvent, paginator: MatPaginator) {
    paginator.pageIndex = event.pageIndex;
    this.artists = null;
    console.log(event.pageIndex);
    this.artists = await this.getArtists(event.pageIndex);
    
    this.artists.forEach(artist => {
      this.artistsSet.add(artist);
    });
  }
}