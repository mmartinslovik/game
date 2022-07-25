import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth-service/auth.service';

export interface IUserForm {
  username: string | null;
  password: string | null;
}

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css'],
})
export class UserLoginComponent implements OnInit {
  form: any = {
    username: null,
    password: null,
  };

  isSuccess: boolean = false;
  errorMessage: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  onSubmit(): void {
    const { username, password } = this.form;
    this.authService.login(username || '', password || '').subscribe({
      next: (data) => {
        this.authService.saveToken(data.data);
        this.isSuccess = true
        this.reloadPage();
      },
      error: (err) => {
        this.errorMessage = err.error.message;
        this.isSuccess = false;
      },
    });
  }

  reloadPage(): void {
    window.location.reload();
  }
}
