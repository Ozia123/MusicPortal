import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';

@Injectable()
export class ApplicationInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let url = 'http://localhost';
        let requestUrl = req.url;

        if (requestUrl.startsWith('web-forms')) {
            requestUrl = requestUrl.replace('web-forms', ':51281/api');
        }
        else {
            url = url + ':63678/';
        }

        req = req.clone({
            url: url + requestUrl
        });

        return next.handle(req);
    }
}
