import { Component, OnInit, Input, Inject } from '@angular/core';
import { TrackModel } from '../models/TrackModel';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UploadDialogComponent } from './upload-dialog/upload-dialog.component';

@Component({
  selector: 'app-track',
  templateUrl: './track.component.html',
  styleUrls: ['./track.component.css']
})
export class TrackComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
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

  onNavigate(route: string) {
    window.open(route, "_blank");
  }
}
