import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { TrackModel } from '../../models/TrackModel';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class PlayerService {
  private trackSource = new BehaviorSubject<TrackModel>(new TrackModel());
  currentTrack = this.trackSource.asObservable();

  public audio: HTMLAudioElement;
  public timeElapsed: BehaviorSubject<number> = new BehaviorSubject(0);
  public timeRemaining: BehaviorSubject<string> = new BehaviorSubject('-00:00');
  public percentElapsed: BehaviorSubject<number> = new BehaviorSubject(0);
  public percentLoaded: BehaviorSubject<number> = new BehaviorSubject(0);
  public playerStatus: BehaviorSubject<string> = new BehaviorSubject('paused');

  constructor() {
    this.audio = new Audio();
    this.attachListeners();
   }

  changeTrack(track: TrackModel) {
    this.trackSource.next(track)
  }

  private attachListeners(): void {
    this.audio.addEventListener('timeupdate', this.calculateTime, false);
    this.audio.addEventListener('playing', this.setPlayerStatus, false);
    this.audio.addEventListener('pause', this.setPlayerStatus, false);
    this.audio.addEventListener('progress', this.calculatePercentLoaded, false);
    this.audio.addEventListener('waiting', this.setPlayerStatus, false);
    this.audio.addEventListener('ended', this.setPlayerStatus, false);
  }

  private calculateTime = (evt) => {
    let ct = this.audio.currentTime;
    let d = this.audio.duration;
    this.setTimeElapsed(ct);
    this.setPercentElapsed(d, ct);
    this.setTimeRemaining(d, ct);
  }

  private calculatePercentLoaded = (evt) => {
    if (this.audio.duration > 0) {
      for (var i = 0; i < this.audio.buffered.length; i++) {
        if (this.audio.buffered.start(this.audio.buffered.length - 1 - i) < this.audio.currentTime) {
          let percent = (this.audio.buffered.end(this.audio.buffered.length - 1 - i) / this.audio.duration) * 100;
          this.setPercentLoaded(percent)
          break;
        }
      }
    }
  }

  private setPlayerStatus = (evt) => {
    switch (evt.type) {
      case 'playing':
        this.playerStatus.next('playing');
        break;
      case 'pause':
        this.playerStatus.next('paused');
        break;
      case 'waiting':
        this.playerStatus.next('loading');
        break;
      case 'ended':
        this.playerStatus.next('ended');
        break;
      default:
        this.playerStatus.next('paused');
        break;
    }
  }

  public getAudio(): HTMLAudioElement {
    return this.audio;
  }

  public setAudio(src: string): void {
    this.audio.src = src;
    this.audio.load();
  }

  public playAudio(): void {
    this.audio.pause();
    this.audio.play();
  }

  public pauseAudio(): void {
    this.audio.pause();
  }

  public seekAudio(position: number): void {
    this.audio.currentTime = position;
  }

  private setTimeElapsed(ct: number): void {
    let seconds   = Math.floor(ct % 60),
      displaySecs = (seconds < 10) ? '0' + seconds : seconds,
      minutes     = Math.floor((ct / 60) % 60),
      displayMins = (minutes < 10) ? '0' + minutes : minutes;

    this.timeElapsed.next(minutes * 60 + seconds);
  }

  private setTimeRemaining(d: number, t: number): void {
    let remaining;
    let timeLeft = d - t,
      seconds = Math.floor(timeLeft % 60) || 0,
      remainingSeconds = seconds < 10 ? '0' + seconds : seconds,
      minutes = Math.floor((timeLeft / 60) % 60) || 0,
      remainingMinutes = minutes < 10 ? '0' + minutes : minutes,
      hours = Math.floor(((timeLeft / 60) / 60) % 60) || 0;
    if (hours === 0) {
      remaining = '-' + remainingMinutes + ':' + remainingSeconds;
    } else {
      remaining = '-' + hours + ':' + remainingMinutes + ':' + remainingSeconds;
    }
    this.timeRemaining.next(remaining);
  }

  private setPercentElapsed(d: number, ct: number): void {
    this.percentElapsed.next(( Math.floor(( 100 / d ) * ct )) || 0 );
  }

  private setPercentLoaded(p): void {
    this.percentLoaded.next(parseInt(p, 10) || 0 );
  }

  public getPercentLoaded(): Observable<number> {
    return this.percentLoaded.asObservable();
  }

  public getPercentElapsed(): Observable<number> {
    return this.percentElapsed.asObservable();
  }

  public getTimeElapsed(): Observable<number> {
    return this.timeElapsed.asObservable();
  }

  public getTimeRemaining(): Observable<string> {
    return this.timeRemaining.asObservable();
  }

  public getPlayerStatus(): Observable<string> {
    return this.playerStatus.asObservable();
  }

  public toggleAudio(): void {
    (this.audio.paused) ? this.audio.play() : this.audio.pause();
  }
}
