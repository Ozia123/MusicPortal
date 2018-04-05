import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { TrackModel } from '../models/TrackModel';
import { HttpQueryStrings } from '../helpers/HttpQueryStrings';

@Injectable()
export class TrackService {
  constructor(
    private http: Http,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string) 
  { 
  }

  private options: any = {
    withCredentials: true
  };

  public async getTopTracks(page: number, itemsPerPage: number): Promise<TrackModel[]> {
    let tracks: TrackModel[] = (await this.http.get('http://localhost:63678/' + HttpQueryStrings.getTopTracks + page + '/' + itemsPerPage, this.options).toPromise()).json();
    return tracks;
  }
}
