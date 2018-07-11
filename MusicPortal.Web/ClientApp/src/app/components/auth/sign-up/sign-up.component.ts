import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user/user.service';
import { Router } from '@angular/router';
import { RegistrationModel } from '../../../models/RegistrationModel';
import { Observable } from 'rxjs/Observable';
import { finalize } from 'rxjs/operators';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  errors: string;
  isRequesting: boolean;
  submitted = false;
  location: string;

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  firstNameFormControl = new FormControl('', [
    Validators.required
  ]);

  lastNameFormControl = new FormControl('', [
    Validators.required
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(6),
    Validators.maxLength(12)
  ]);

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    if (window.navigator.geolocation) {
      window.navigator.geolocation.getCurrentPosition(location => this.location = location.timestamp.toString());
    }
  }

  signUp({ value, valid }: { value: RegistrationModel, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';

    // TODO: get values from form
    value.email = this.emailFormControl.value;
    value.firstName = this.firstNameFormControl.value;
    value.lastName = this.lastNameFormControl.value;
    value.password = this.passwordFormControl.value;
    value.location = this.location;

    if (valid) {
      this.userService.signUp(value)
      .pipe(finalize(() => this.isRequesting = false))
      .subscribe(
        result => {
          this.router.navigate(['/login'], {queryParams: { email: value.email }});
        },
        errors => {
          this.errors = errors;
        }
      );
    }
  }
}
