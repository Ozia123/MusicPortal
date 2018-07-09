import { Component, OnInit, Inject } from '@angular/core';
import { MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TrackModel } from '../../../models/TrackModel';
import { TrackService } from '../../../services/track/track.service';
import { AngularFireStorage, AngularFireUploadTask } from 'angularfire2/storage';
import { Observable } from 'rxjs/Observable';
import { AngularFirestore } from 'angularfire2/firestore';
import { catchError, map, tap } from 'rxjs/operators';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-upload-dialog',
  templateUrl: './upload-dialog.component.html',
  styleUrls: ['./upload-dialog.component.css'],
  providers: [ TrackService ]
})
export class UploadDialogComponent {
  task: AngularFireUploadTask;
  percentage: Observable<number>;
  snapshot: Observable<any>;
  downloadURL: Observable<string>;
  isHovering: boolean;
  uploadStarted: boolean = false;
  fileName: string;

  pct: number = 0;

  constructor(
    public dialogRef: MatDialogRef<UploadDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private trackService: TrackService,
    private storage: AngularFireStorage,
    private db: AngularFirestore,
    public snackBar: MatSnackBar
  ) { }

  toggleHover(event: boolean) {
    this.isHovering = event;
  }

  async startUpload(event: FileList) {
    const file = event.item(0)
    if (file.type.split('/')[0] !== 'audio') {
      console.error('unsupported file type :( ')
      return;
    }
    this.uploadStarted = true;
    this.fileName = file.name;
    const path = `tracks/${new Date().getTime()}_${file.name}`;
    const customMetadata = { app: this.data.track.name };
    this.task = this.storage.upload(path, file, { customMetadata })
    this.percentage = this.task.percentageChanges();
    this.snapshot = this.task.snapshotChanges().pipe(
      tap(snap => {
        if (snap.bytesTransferred === snap.totalBytes) {
          this.sendUpdate();
        }
      })
    );
    this.downloadURL = this.task.downloadURL(); 
  }

  isActive(snapshot) {
    return snapshot.state === 'running' && snapshot.bytesTransferred < snapshot.totalBytes
  }

  private async sendUpdate() {
    this.data.track.cloudURL = await this.downloadURL.toPromise();
    let track: TrackModel = await this.trackService.updateTrack(this.data.track);
    if (track == null) {
      this.showMessage('Error!');
    }
    else {
      this.showMessage('Uploaded!');
    }
    this.dialogRef.close(this.data.track);
  }

  onCancel(): void {
    this.task.cancel();
    this.showMessage('Upload canceled');
    this.uploadStarted = false;
    this.snapshot = null;
    this.percentage = null;
    this.fileName = null;
  }

  onClose(): void {
    if (this.uploadStarted) {
      this.task.cancel();
      this.showMessage('Upload canceled');
    }

    this.dialogRef.close(this.data.track);
  }

  private showMessage(message: string) {
    this.snackBar.open(message, 'Ok', {
      duration: 2000,
    });
  }
}
