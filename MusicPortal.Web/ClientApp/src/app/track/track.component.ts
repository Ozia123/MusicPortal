import { Component, OnInit, Input, Inject } from '@angular/core';
import { TrackModel } from '../models/TrackModel';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UploadDialogComponent } from './upload-dialog/upload-dialog.component';
import { PlayerService } from '../services/player.service';
import { TrackService } from '../services/track.service';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-track',
  templateUrl: './track.component.html',
  styleUrls: ['./track.component.css']
})
export class TrackComponent implements OnInit {
  isPlaying: boolean = false;

  constructor(public dialog: MatDialog, private playerService: PlayerService) { }

  ngOnInit() {
    if (this.track.cloudURL != null) {
      this.checkIfPlaying();
    }
  }
  @Input() public track: TrackModel;

  showUploadDialog() {
    let dialogRef = this.dialog.open(UploadDialogComponent, {
      width: '500px',
      data: { track: this.track }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.track = result || this.track
    });
  }

  checkIfPlaying() {
    this.playerService.getPlayerStatus().subscribe(
      status => {
        if (status === 'paused' || this.track.cloudURL != this.playerService.getAudio().src) {
          this.isPlaying = false;
        }
        else {
          this.isPlaying = true;
        }
        if (status == 'ended') {
          this.isPlaying = false;
        }
      }
    );
  }

  onPlay() {
    this.isPlaying = true;
    this.playerService.changeTrack(this.track);
  }

  onNavigate(route: string) {
    window.open(route, "_blank");
  }
}
