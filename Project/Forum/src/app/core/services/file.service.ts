import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Token } from '../types/web.types';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  constructor(private http: HttpClient, private router: Router) {}

  readonly APIUrl = environment.baseUrls.server + environment.baseUrls.apiUrl;

  token?: string;

  addFile(file: any) {
    const headers = new HttpHeaders({
      'Content-Type': 'multipart/form-data',
    });
    return this.http.post<any>(this.APIUrl + 'file/uploadfile', file, {
      reportProgress: true,
      observe: 'events',
    });
  }
}
