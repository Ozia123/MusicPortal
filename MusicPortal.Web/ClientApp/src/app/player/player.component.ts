import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { TrackModel } from '../models/TrackModel';
import { PlayerService } from '../services/player.service';
import { TaskEvent } from '@firebase/storage-types';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {
  track: TrackModel = null;
  value: number = 0;
  isAudio = false;
  isPlaying = true;

  constructor(private playerService: PlayerService) { }

  ngOnInit() {
    this.playerService.currentTrack.subscribe(track => {
      if (!this.track) {
        this.track = track;
      }

      if (this.track.name == track.name && this.isAudio) {
        this.resumeAudio();
      }
      else {
        this.track = track;
        this.playAudio();
      }

      this.isAudio = (this.track.name || '') !== '';
    });
  }

  private playAudio() {
    this.playerService.setAudio(this.track.cloudURL);
    this.playerService.playAudio();
    this.playerService.getPlayerStatus().subscribe(
      status => {
        if (status === 'ended') {
          this.isAudio = false;
          this.isPlaying = false;
        }
        if (status === 'paused') {
          this.isPlaying = false;
        }
        else {
          this.isPlaying = true;
        }
      }
    );
    this.playerService.getTimeElapsed().subscribe(
      timeElapsed => {
        this.value = timeElapsed / this.playerService.getAudio().duration * 100;
      } 
    );
  }

  private resumeAudio() {
    this.playerService.toggleAudio();
  }

  onToggle() {
    this.isPlaying = !this.isPlaying;
    this.playerService.toggleAudio();
  }

  onChangePercentage() {
    this.playerService.seekAudio(this.value / 100 * this.playerService.getAudio().duration);
  }
}
