import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { AlbumModel } from '../models/AlbumModel';
import { HttpQueryStrings } from '../helpers/HttpQueryStrings';

@Injectable()
export class AlbumService {
  constructor(
    private http: Http,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string) 
  { }

  private options: any = {
    withCredentials: true
  };

  public async getTopArtistsAlbums(name: string, page: number, itemsPerPage: number): Promise<AlbumModel[]> {
    let albums: AlbumModel[] = (await this.http.get('http://localhost:63678/' + HttpQueryStrings.getTopArtistsAlbums + name + '/' + page + '/' + itemsPerPage, this.options).toPromise()).json();
    return albums;
  }
}
