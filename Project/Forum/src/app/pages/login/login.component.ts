import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login, Token } from 'src/app/core/types/web.types';
import { UserService } from 'src/app/core/services/user.service';
import Swal from 'sweetalert2';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(
    private userService: UserService,
    private router: Router,
    private fb: FormBuilder,
    private appComponent: AppComponent
  ) {
    this.loginForm = this.fb.group({
      login: ['', [Validators.required]],
      password: ['', Validators.required],
    });
  }

  loginForm: FormGroup;
  fieldTextType: boolean = false;

  request: Login = {
    login: '',
    password: '',
  };

  token?: Token;

  ngOnInit() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('userId');
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  login() {
    this.request = {
      login: this.loginForm.value.login,
      password: this.loginForm.value.password,
    };

    this.userService.login(this.request).subscribe({
      next: (data) => {
        this.token = data;

        localStorage.setItem('token', this.token.token);
        localStorage.setItem('role', this.token.role);
        localStorage.setItem('userId', this.token.id);
        this.router.navigate(['/home']).then(() => {
          this.appComponent.signedIn = true;
          window.location.reload();
        });
      },
      error: (error) => {
        console.log(error);
        localStorage.clear();
        this.displayStatus('Neteisingas prisijungimo vardas arba slaptažodis');
      },
    });
  }

  displayStatus(text: string) {
    Swal.fire({
      icon: 'error',
      title: '',
      text: text,
    });
  }
}
