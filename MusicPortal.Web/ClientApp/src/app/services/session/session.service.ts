import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ApiRoutes } from '../../helpers/ApiRoutes';
import { catchError, retry } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import { HttpHeaders } from '@angular/common/http';
import { BaseService } from '../base.service';

@Injectable()
export class SessionService extends BaseService {
    private options: any = {
        withCredentials: true
    };

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'http://localhost:63678/about',
        })
    };

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        super();
    }

    public setAuthData(): Observable<any> {
        const token = { token: localStorage.getItem('auth_token') };

        return this.http.post(ApiRoutes.setAuthData, token, this.httpOptions).pipe(
            catchError(error => this.handleError(error))
        );
    }
}
