import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { TrackModel } from '../../models/TrackModel';
import { ApiRoutes } from '../../helpers/ApiRoutes';

@Injectable()
export class TrackService {
  constructor(
    private http: Http,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string) { }

  private options: any = {
    withCredentials: true
  };

  public async getTopTracks(page: number, itemsPerPage: number): Promise<TrackModel[]> {
    let tracks: TrackModel[] = (await this.http.get(this.baseUrl + ApiRoutes.getTopTracks + page + '/' + itemsPerPage, this.options).toPromise()).json();
    return tracks;
  }

  public async getCountOfTracks(): Promise<number> {
    var object = (await this.http.get(this.baseUrl + ApiRoutes.getCountOfTracks, this.options).toPromise()).json();
    return object.count;
  }

  public async getTopArtistsTracks(artistName: string, page: number, itemsPerPage: number): Promise<TrackModel[]> {
    let tracks: TrackModel[] = (await this.http.get(this.baseUrl + ApiRoutes.getTopArtistsTracks + artistName + '/' + page + '/' + itemsPerPage, this.options).toPromise()).json();
    return tracks;
  }

  public async getAlbumTracks(albumName: string): Promise<TrackModel[]> {
    let tracks: TrackModel[] = (await this.http.get(this.baseUrl + ApiRoutes.getAlbumTracks + albumName, this.options).toPromise()).json();
    return tracks;
  }

  public async updateTrack(track: TrackModel): Promise<TrackModel> {
    track = (await this.http.post(this.baseUrl + ApiRoutes.updateTrack, track, this.options).toPromise()).json();
    return track;
  }
}
