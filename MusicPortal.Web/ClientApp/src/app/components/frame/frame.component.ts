import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppConfig } from '../../helpers/AppConfig';
import { TrackEventListenerService } from '../../services/dom-event-listeners/track-event-listener.service';
import { SessionService } from '../../services/session/session.service';
import { Observable } from 'rxjs/Observable';
import * as $ from 'jquery';

@Component({
  selector: 'app-frame',
  templateUrl: './frame.component.html',
  styleUrls: ['./frame.component.css']
})
export class FrameComponent implements AfterViewInit  {
  public pageUrl: string;
  public isLoading = true;

  constructor(private router: Router, private trackEventListenerService: TrackEventListenerService, private sessionService: SessionService) {
    this.resolveUrl(router.url);
    this.setAuthData();
  }

  private resolveUrl(url: string) {
    if (localStorage.getItem('auth_token') !== '') {
      url += '?auth_token=' + localStorage.getItem('auth_token');
    }

    this.pageUrl = AppConfig.WebFormsBaseUrl + url;
  }

  private setAuthData() {
    const that = this;

    $(window).on('load', () => {
      this.isLoading = false;

      // that.sessionService.setAuthData().subscribe(
      //   result => { console.log(result); },
      //   errors => { console.log(errors); }
      // );
    });
  }

  ngAfterViewInit() {
    this.trackEventListenerService.onClick().then(observable => observable.subscribe(console.log, console.log));
  }
}
