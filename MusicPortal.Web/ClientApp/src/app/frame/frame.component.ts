import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppConfig } from '../helpers/AppConfig';

@Component({
  selector: 'app-frame',
  templateUrl: './frame.component.html',
  styleUrls: ['./frame.component.css']
})
export class FrameComponent implements AfterViewInit  {
  public pageUrl: string;
  public isLoading = true;

  constructor(private router: Router) {
    this.resolveUrl(router.url);
  }

  resolveUrl(url: string) {
    const formUrl = url + '.aspx';

    this.pageUrl = AppConfig.WebFormsBaseUrl + formUrl;
  }

  ngAfterViewInit() {
    this.isLoading = false;
  }

}
