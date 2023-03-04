import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ChangePassword, Login, Token, GroupPost } from '../types/web.types';

@Injectable({
  providedIn: 'root',
})
export class GroupPostService {
  constructor(private http: HttpClient, private router: Router) {}

  readonly APIUrl = environment.baseUrls.server + environment.baseUrls.apiUrl;

  token?: string;

  getGroupPostsById(id: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<any>(this.APIUrl + 'GroupPost/' + id, {
      headers: headers,
    });
  }

  addGroupPost(groupPost: GroupPost): Observable<GroupPost> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.post<GroupPost>(this.APIUrl + 'GroupPost', groupPost, {
      headers: headers,
    });
  }

  deleteGroupPost(id: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(this.APIUrl + 'GroupPost/' + id, {
      headers: headers,
    });
  }
}
