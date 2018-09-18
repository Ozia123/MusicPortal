import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import * as $ from 'jquery';
import { contents } from 'jquery';

@Injectable()
export class TrackEventListenerService {
  obs: Observable<any>;

  constructor() {
  }

  private init(selector: string): Promise<any> {
    const that = this;

    return new Promise(function cb(resolvePromise) {
      setTimeout(function () {
        const element = $('iframe').contents().find('div');
        if (element == null || element.length == null || element.length === 0) {
            cb(resolvePromise);
        } else {
          console.log(element);
          resolvePromise({ element: element, selector: selector });
        }
      }, 250);
    });
  }

  onClick(): Promise<Observable<any>> {
    const that = this;

    return new Promise(function cb(resolvePromise) {
      that.init('iframe').then(data => {
        that.obs = Observable.fromEvent(document.querySelector('iframe').contentWindow.document, 'click');
        resolvePromise(that.obs);
      });
    });
  }
}
