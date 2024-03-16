import { Component } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { Login } from '../../Models/Login';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiErrorResponse } from '../../Models/ApiErrorResponse';
import { ApiError } from '../../Models/ApiError';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  usersLoginData!: Login;
  errorMessage!: ApiErrorResponse;
  userForm!: FormGroup;
  constructor(
    private usersService: UsersService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.userForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  login() {
    if (this.userForm.valid) {
      this.usersService
        .login(
          this.userForm.controls['username'].value,
          this.userForm.controls['password'].value
        )
        .subscribe({
          next: (response) => {
            this.usersLoginData = response.data;
            localStorage.setItem(
              'access_token',
              this.usersLoginData.accessToken
            );
            localStorage.setItem('username', this.usersLoginData.username);
            localStorage.setItem('role', this.usersLoginData.role);
            if (this.usersLoginData.role == 'Admin') {
              this.router.navigate(['/admin/home']);
            } else {
              this.router.navigate(['/customer/home']);
            }
          },
          error: (response) => {
            this.errorMessage = response.error;
          },
        });
    }
  }
}
