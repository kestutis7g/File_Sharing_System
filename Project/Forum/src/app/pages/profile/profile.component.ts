import { Component, Input, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { FileInfo, User } from 'src/app/core/types/web.types';
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
  profilePictureName?: string;

  files: FileInfo[] = [];

  fileClass = 'fa-solid fa-file-video';

  ngOnInit() {
    this.profilePicture =
      'https://localhost:7111/api/user/' +
      localStorage.getItem('userId')! +
      '/profilePicture';

    this.fileService
      .getUserFileList(localStorage.getItem('userId')!)
      .subscribe({
        next: (data) => {
          this.files = data;
          this.files.sort((a, b) => a.name.localeCompare(b.name));
          console.log(data);
          this.setFileIcons();
        },
        error: (error) => {
          console.log(error);
        },
      });

    this.userService.getUserById(localStorage.getItem('userId')!).subscribe({
      next: (data) => {
        this.user = data;
        console.log(data);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  setFileIcons() {
    this.files.forEach((file) => {
      if (file.fileMime.startsWith('image/')) {
        file.icon = 'fa-solid fa-file-image fa-2xl';
        file.canOpen = true;
      } else if (file.fileMime.startsWith('video/')) {
        file.icon = 'fa-solid fa-file-video fa-2xl';
        file.canOpen = true;
      } else if (file.fileMime.startsWith('audio/')) {
        file.icon = 'fa-solid fa-file-audio fa-2xl';
        file.canOpen = true;
      } else if (file.fileMime.startsWith('application/pdf')) {
        file.icon = 'fa-solid fa-file-pdf fa-2xl';
        file.canOpen = true;
      } else if (
        file.fileMime.startsWith(
          'application/vnd.openxmlformats-officedocument.spreadsheetml'
        )
      ) {
        file.icon = 'fa-solid fa-file-excel fa-2xl';
        file.canOpen = false;
      } else if (
        file.fileMime.startsWith(
          'application/vnd.openxmlformats-officedocument.presentationml'
        )
      ) {
        file.icon = 'fa-solid fa-file-powerpoint fa-2xl';
        file.canOpen = false;
      } else if (
        file.fileMime.startsWith('application/msword') ||
        file.fileMime.startsWith(
          'application/vnd.openxmlformats-officedocument.wordprocessingml'
        )
      ) {
        file.icon = 'fa-solid fa-file-word fa-2xl';
        file.canOpen = false;
      } else if (
        file.fileMime.startsWith('application/octet-stream') ||
        file.fileMime.startsWith('application/zip') ||
        file.fileMime.startsWith('application/x-zip-compressed') ||
        file.fileMime.startsWith('application/vnd.rar')
      ) {
        file.icon = 'fa-solid fa-file-zipper fa-2xl';
        file.canOpen = false;
      } else if (file.fileMime.startsWith('text/csv')) {
        file.icon = 'fa-solid fa-file-csv fa-2xl';
        file.canOpen = false;
      } else if (file.fileMime.startsWith('text/')) {
        file.icon = 'fa-solid fa-file-lines fa-2xl';
        file.canOpen = true;
      } else if (file.fileMime.startsWith('application/x-msdownload')) {
        file.icon = 'fa-solid fa-file-code fa-2xl';
        file.canOpen = false;
      } else {
        file.icon = 'fa-solid fa-file fa-2xl';
        file.canOpen = false;
      }
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
            console.log(data);
            this.reloadProfilePicture();
          },
          error: (error) => {
            console.log(error);
          },
        });
    }
  }

  sortFiles(sortBy: string) {
    if (sortBy == 'nameUp') {
      this.files.sort((a, b) => a.name.localeCompare(b.name));
    } else if (sortBy == 'nameDown') {
      this.files.sort((a, b) => b.name.localeCompare(a.name));
    } else if (sortBy == 'sizeUp') {
      this.files.sort((a, b) => (a.size > b.size ? 1 : -1));
    } else if (sortBy == 'sizeDown') {
      this.files.sort((a, b) => (a.size > b.size ? -1 : 1));
    } else if (sortBy == 'typeUp') {
      this.files.sort((a, b) => a.fileMime.localeCompare(b.fileMime));
    } else if (sortBy == 'typeDown') {
      this.files.sort((a, b) => b.fileMime.localeCompare(a.fileMime));
    }
  }

  reloadProfilePicture() {
    this.profilePicture = '';
    setTimeout(() => {
      this.profilePicture =
        'https://localhost:7111/api/user/' +
        localStorage.getItem('userId')! +
        '/profilePicture';
    }, 100);
  }

  downloadFile(id: string) {
    window.open(
      'https://localhost:7111/api/file/' + id + '?action=download',
      '_blank'
    );
  }

  previewFile(id: string) {
    window.open(
      'https://localhost:7111/api/file/' + id + '?action=preview',
      '_blank'
    );
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
