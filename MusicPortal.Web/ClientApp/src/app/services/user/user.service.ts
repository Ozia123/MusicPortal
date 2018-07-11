import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { RegistrationModel } from '../../models/RegistrationModel';
import { ApiRoutes } from '../../helpers/ApiRoutes';
import { BaseService } from '../base.service';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import { CredentialsModel } from '../../models/CredentialsModel';

@Injectable()
export class UserService extends BaseService {
  private loggedIn = false;
  private authNavStatusSource = new BehaviorSubject<boolean>(false);

  constructor(
    private http: Http,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string) {
      super();
      this.loggedIn = !!localStorage.getItem('auth_token');
      this.authNavStatusSource.next(this.loggedIn);
    }

  private options: any = {
    withCredentials: true
  };

  public signUp(model: RegistrationModel): Observable<RegistrationModel> {
    return this.http.post(this.baseUrl + ApiRoutes.signUp, model, this.options)
      .map(res => true)
      .pipe(catchError(error => this.handleError(error)));
  }

  public login(model: CredentialsModel): Observable<any> {
    return this.http.post(this.baseUrl + ApiRoutes.login, model)
    .map(res => res.json())
    .map(res => {
      localStorage.setItem('auth_token', res.auth_token);
      this.loggedIn = true;
      this.authNavStatusSource.next(true);
      return true;
    })
    .pipe(catchError(error => this.handleError(error)));
  }

  public logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this.authNavStatusSource.next(false);
  }

  public isLoggedIn(): boolean {
    return this.loggedIn;
  }
}
