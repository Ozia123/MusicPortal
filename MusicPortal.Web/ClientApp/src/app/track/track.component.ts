import { Component, OnInit, Input } from '@angular/core';
import { TrackModel } from '../models/TrackModel';

@Component({
  selector: 'app-track',
  templateUrl: './track.component.html',
  styleUrls: ['./track.component.css']
})
export class TrackComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  @Input() public track: TrackModel;

  onNavigate(route: string) {
    window.open(route, "_blank");
  }
}
