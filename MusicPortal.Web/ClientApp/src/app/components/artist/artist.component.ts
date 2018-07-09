import { Component, Input, OnInit } from '@angular/core';
import { ArtistModel } from '../../models/ArtistModel';

@Component({
  selector: 'app-artist',
  templateUrl: './artist.component.html',
  styleUrls: ['./artist.component.css']
})
export class ArtistComponent implements OnInit {
  constructor() { }

  ngOnInit() {
  }
  @Input() public artist: ArtistModel;
}
