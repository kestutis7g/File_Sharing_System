import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/core/types/web.types';
import { UserService } from 'src/app/core/services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  constructor(
    private userService: UserService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.userService.getUserDefaults().subscribe({
      next: (data) => {
        this.user = data;
      },
    });
  }
  registerForm: FormGroup = new FormGroup({
    login: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
    role: new FormControl<string>('User', [Validators.required]),
    name: new FormControl<string>('', [Validators.required]),
    lastname: new FormControl<string>('', [Validators.required]),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    phone: new FormControl<string>('', [Validators.required]),
  });

  fieldTextType: boolean = false;

  repeatFieldTextType: boolean = false;

  user?: User;

  ngOnInit() {}

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  toggleRepeatFieldTextType() {
    this.repeatFieldTextType = !this.repeatFieldTextType;
  }

  register() {
    this.userService.addUser(this.registerForm.value).subscribe({
      next: () => {
        this.router.navigate(['/login']).then(() => {});
      },
      error: (error) => {
        Swal.fire({
          icon: 'error',
          title: error.status,
          text: 'Nepavyko sukurti paskyros',
        });
        console.log(error);
      },
    });
  }
}
