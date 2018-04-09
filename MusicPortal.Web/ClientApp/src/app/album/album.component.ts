import { Component, OnInit, Input } from '@angular/core';
import { AlbumModel } from '../models/AlbumModel';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {
  
  constructor() { }

  ngOnInit() {
  }
  @Input() public album: AlbumModel;
}
