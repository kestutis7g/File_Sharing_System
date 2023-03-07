import { Component, Input, OnInit } from '@angular/core';
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
import { Subscription } from 'rxjs';
import { FileService } from 'src/app/core/services/file.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  constructor(
    private userService: UserService,
    private fileService: FileService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.userService.getUserDefaults().subscribe({
      next: (data) => {
        this.user = data;
      },
    });
  }

  profileForm: FormGroup = new FormGroup({
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

  profilePicture?: string;

  ngOnInit() {
    this.profilePicture =
      'https://localhost:7111/api/user/' +
      localStorage.getItem('userId')! +
      '/profilePicture';

    this.fileService
      .getUserFileList(localStorage.getItem('userId')!)
      .subscribe({
        next: (data) => {
          console.log(data);
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    console.log(file.name);

    if (file) {
      const formData = new FormData();
      formData.append('File', file);

      this.userService
        .updateProfilePicture(localStorage.getItem('userId')!, formData)
        .subscribe({
          next: (data) => {
            window.location.reload();
          },
          error: (error) => {
            console.log(error);
          },
        });
    }
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  toggleRepeatFieldTextType() {
    this.repeatFieldTextType = !this.repeatFieldTextType;
  }

  profile() {
    this.userService.addUser(this.profileForm.value).subscribe({
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
