import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ChangePassword, Login, Token, Comment } from '../types/web.types';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  constructor(private http: HttpClient, private router: Router) {}

  readonly APIUrl = environment.baseUrls.server + environment.baseUrls.apiUrl;

  token?: string;

  getCommentList(): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get(this.APIUrl + 'Comment', { headers: headers });
  }

  getCommentById(id: string): Observable<Comment> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<Comment>(this.APIUrl + 'Comment/' + id, {
      headers: headers,
    });
  }

  addComment(comment: Comment): Observable<Comment> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.post<Comment>(this.APIUrl + 'Comment', comment, {
      headers: headers,
    });
  }

  updateComment(comment: Comment) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.put(this.APIUrl + 'Comment/' + comment.id, comment, {
      headers: headers,
    });
  }

  deleteComment(id: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(this.APIUrl + 'Comment/' + id, {
      headers: headers,
    });
  }

  hardDeleteComment(id: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(this.APIUrl + 'Comment/' + id + '/hard', {
      headers: headers,
    });
  }
}
