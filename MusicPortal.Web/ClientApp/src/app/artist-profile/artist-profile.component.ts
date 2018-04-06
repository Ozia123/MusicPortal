import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../services/artist.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ArtistModel } from '../models/ArtistModel';
import { AlbumComponent } from '../album/album.component';
import { AlbumModel } from '../models/AlbumModel';
import { AlbumService } from '../services/album.service';

@Component({
  selector: 'app-artist-profile',
  templateUrl: './artist-profile.component.html',
  styleUrls: ['./artist-profile.component.css'],
  providers: [ArtistService]
})
export class ArtistProfileComponent implements OnInit {
  private albumsPage = 1;

  artist: ArtistModel = null;
  albums: AlbumModel[] = null;

  constructor(
    private router: Router, 
    private route: ActivatedRoute, 
    private artistService: ArtistService,
    private albumService: AlbumService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.Init();
    });
  }

  private async Init() {
    let artistName: string = this.route.snapshot.paramMap.get('name') || '';
    if (artistName == '') {
      this.router.navigate(['']);
    }
    await this.setArtist(artistName);
    await this.setAlbums();
  }

  private async setArtist(name: string) {
    this.artist = await this.getArtist(name);
    if (this.artist == null) {
      this.router.navigate(['']);
    }
  }

  private async getArtist(name: string): Promise<ArtistModel> {
    return await this.artistService.getFullInfoArtist(name);
  }

  private async setAlbums() {
    this.albums = await this.getTopAlbums(1);
  }

  private async getTopAlbums(pageIndex: number): Promise<AlbumModel[]> {
    return await this.albumService.getTopArtistsAlbums(this.artist.name, pageIndex);
  }

  async onMoreAlbums() {
    let newAlbums: AlbumModel[] = await this.getTopAlbums(++this.albumsPage);
    this.albums = this.albums.concat(newAlbums);
  }
}
