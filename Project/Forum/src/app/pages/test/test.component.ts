import { Component, Input, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/core/types/web.types';
import { FileService } from 'src/app/core/services/file.service';
import Swal from 'sweetalert2';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss'],
})
export class TestComponent implements OnInit {
  @Input()
  requiredFileType = '';

  fileName = '';
  uploadProgress = 0;
  uploadSub = new Subscription();

  constructor(private http: HttpClient, private fileService: FileService) {}

  ngOnInit() {}

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    console.log(file.name);

    if (file) {
      this.fileName = file.name;
      const formData = new FormData();
      formData.append('Location', '/test');
      formData.append('Visibility', 'public');
      formData.append('File', file);

      this.fileService.addFile(formData).subscribe({
        next: (data) => {},
        error: (error) => {
          console.log(error);
        },
      });

      // const upload$ = this.http.post('/api/thumbnail-upload', formData, {
      //   reportProgress: true,
      //   observe: 'events',
      // });
      //.pipe(finalize(() => this.reset()));

      // this.uploadSub = upload$.subscribe((event) => {
      //   if (event.type == HttpEventType.UploadProgress) {
      //     this.uploadProgress = Math.round(100 * (event.loaded / event.total));
      //   }
      // });
    }
  }

  // cancelUpload() {
  //   this.uploadSub.unsubscribe();
  //   this.reset();
  // }

  // reset() {
  //   this.uploadProgress = null;
  //   this.uploadSub = undefined;
  // }
}
