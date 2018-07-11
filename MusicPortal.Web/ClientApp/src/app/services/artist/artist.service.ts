import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { ArtistModel } from '../../models/ArtistModel';
import { ApiRoutes } from '../../helpers/ApiRoutes';

@Injectable()
export class ArtistService {
  constructor(
    private http: Http,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string) 
  { }

  private options: any = {
    withCredentials: true
  };

  public async getTopArtists(page: number, itemsPerPage: number): Promise<ArtistModel[]> {
    let artists: ArtistModel[] = (await this.http.get(this.baseUrl + ApiRoutes.getTopArtists + page + '/' + itemsPerPage, this.options).toPromise()).json();
    return artists;
  }

  public async getCountOfArtists(): Promise<number> {
    var object = (await this.http.get(this.baseUrl + ApiRoutes.getCountOfArtists, this.options).toPromise()).json();
    return object.count;
  }

  public async getFullInfoArtist(name: string): Promise<ArtistModel> {
    let artist: ArtistModel = (await this.http.get(this.baseUrl + ApiRoutes.getFullInfoArtist + name, this.options).toPromise()).json();
    return artist;
  }

  public async getSimilarArtists(name: string): Promise<ArtistModel[]> {
    let artists: ArtistModel[] = (await this.http.get(this.baseUrl + ApiRoutes.getSimilarArtists + name, this.options).toPromise()).json();
    return artists;
  }
}