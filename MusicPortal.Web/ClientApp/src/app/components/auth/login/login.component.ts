import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserService } from '../../../services/user/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { CredentialsModel } from '../../../models/CredentialsModel';
import { Observable } from 'rxjs/Observable';
import { finalize } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  private subscription: Subscription;

  errors: string;
  isRequesting: boolean;
  submitted = false;

  emailFormControl = new FormControl('', []);
  passwordFormControl = new FormControl('', []);

  constructor(private userService: UserService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.subscription = this.activatedRoute.queryParams.subscribe(params => {
      this.emailFormControl.setValue(params.email);
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  login({ value, valid }: { value: CredentialsModel, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';

    value.email = this.emailFormControl.value;
    value.password = this.passwordFormControl.value;

    this.userService.login(value)
      .pipe(finalize(() => this.isRequesting = false))
      .subscribe(
        result => {
          this.router.navigate(['/']);
        },
        errors => {
          this.errors = errors;
        }
      );
  }
}
