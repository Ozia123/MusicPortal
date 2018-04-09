import { ArtistModel } from '../../models/ArtistModel';
import { Component, OnInit, ViewChild } from '@angular/core';
import { PageEvent, MatPaginator } from '@angular/material';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { ArtistService } from '../../services/artist.service';

import { ArtistComponent } from '../../artist/artist.component';

@Component({
  selector: 'app-artists',
  templateUrl: './artists.component.html',
  styleUrls: ['./artists.component.css'],
  providers: [ArtistService]
})
export class ArtistsComponent implements OnInit {
  artists: ArtistModel[] = null;

  private paginator: MatPaginator;

  pageIndex: number = 0;
  length: number = 100;
  pageSize: number = 20;
  pageSizeOptions: number[] = [10, 20, 30];

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
  }

  constructor(private router: Router, private route: ActivatedRoute, private artistService: ArtistService) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.pageIndex = this.getPageIndex();
      this.pageSize = this.getPageSize();
      this.getArtists();
    });
  }

  private getPageIndex(): number {
    let pageStr: string = this.route.snapshot.paramMap.get('page') || '1';
    let page: number = Number(pageStr) || 1
    return page - 1;
  }

  private getPageSize(): number {
    let pageSizeStr = localStorage.getItem('artistsPageSize');
    if (!pageSizeStr) {
      localStorage.setItem('artistsPageSize', '20');
      return 20;
    }
    return Number(pageSizeStr) || 20;
  }

  async getArtists() {
    this.artists = await this.artistService.getTopArtists(this.pageIndex + 1, this.pageSize);
  }

  changePage(event: PageEvent) {
    if (event.pageSize != this.getPageSize()) {
      this.changePageSize(event.pageSize);
      return;
    }
    this.router.navigate(['chart/artists/' + (event.pageIndex + 1)]);
  }

  changePageSize(newPageSize: number) {
    localStorage.setItem('artistsPageSize', newPageSize.toString());
    this.pageSize = newPageSize;
    this.getArtists();
  }
}
