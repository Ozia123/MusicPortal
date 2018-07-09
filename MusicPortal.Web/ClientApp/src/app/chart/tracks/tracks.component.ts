import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { TrackService } from '../../services/track.service';
import { TrackModel } from '../../models/TrackModel';

import { TrackComponent } from '../../track/track.component';

@Component({
  selector: 'app-tracks',
  templateUrl: './tracks.component.html',
  styleUrls: ['./tracks.component.css']
})
export class TracksComponent implements OnInit {
  tracks: TrackModel[] = null;

  private paginator: MatPaginator;

  pageIndex: number = 0;
  length: number = 100;
  pageSize: number = 20;
  pageSizeOptions: number[] = [10, 20, 30];

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
  }

  constructor(private router: Router, private route: ActivatedRoute, private trackService: TrackService) {
  }

  async ngOnInit() {
    this.route.params.subscribe(params => {
      this.tracks = null;
      this.pageIndex = this.getPageIndex();
      this.pageSize = this.getPageSize();
      this.getPagination();
      this.getTracks();
    });
  }

  getPageIndex(): number {
    let pageStr: string = this.route.snapshot.paramMap.get('page') || '1';
    let page: number = Number(pageStr) || 1
    return page - 1;
  }

  getPageSize(): number {
    let pageSizeStr = localStorage.getItem('tracksPageSize');
    if (!pageSizeStr) {
      localStorage.setItem('tracksPageSize', '20');
      return 20;
    }
    return Number(pageSizeStr) || 20;
  }

  async getPagination() {
    let tracksCountStr = localStorage.getItem('tracksCount');

    if (!tracksCountStr) {
      this.length = await this.trackService.getCountOfTracks();
    }
    else {
      this.length = Number(tracksCountStr) || 100;
    }
    localStorage.setItem('tracksCount', this.length.toString());
  }

  async getTracks() {
    this.tracks = await this.trackService.getTopTracks(this.pageIndex + 1, this.pageSize);
  }

  changePage(event: PageEvent) {
    if (event.pageSize != this.getPageSize()) {
      this.changePageSize(event.pageSize);
      return;
    }
    this.router.navigate(['chart/tracks/' + (event.pageIndex + 1)]);
  }

  changePageSize(newPageSize: number) {
    localStorage.setItem('tracksPageSize', newPageSize.toString());
    this.pageSize = newPageSize;
    this.getTracks();
  }
}
