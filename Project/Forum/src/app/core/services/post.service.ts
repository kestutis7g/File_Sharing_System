import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ChangePassword, Login, Token, Post } from '../types/web.types';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(private http: HttpClient, private router: Router) {}

  readonly APIUrl = environment.baseUrls.server + environment.baseUrls.apiUrl;

  token?: string;

  getPostDefaults() {
    return of({
      title: '',
      content: '',
      picture: '',
      type: '',
      userId: '',
      createdAt: undefined,
      modifiedAt: undefined,
      deleted: undefined,
      banned: undefined,
    } as Post);
  }

  getPostList(): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get(this.APIUrl + 'Post', { headers: headers });
  }

  getPostById(id: string): Observable<Post> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<Post>(this.APIUrl + 'Post/' + id, {
      headers: headers,
    });
  }

  getCommentsByPostId(id: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<any>(this.APIUrl + 'Post/' + id + '/comments', {
      headers: headers,
    });
  }

  addPost(post: Post): Observable<Post> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.post<Post>(this.APIUrl + 'Post', post, {
      headers: headers,
    });
  }

  updatePost(post: Post) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.put(this.APIUrl + 'Post/' + post.id, post, {
      headers: headers,
    });
  }

  deletePost(id: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(this.APIUrl + 'Post/' + id, {
      headers: headers,
    });
  }

  hardDeletePost(id: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(this.APIUrl + 'Post/' + id + '/hard', {
      headers: headers,
    });
  }
}
