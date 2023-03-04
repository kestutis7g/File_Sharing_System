import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ChangePassword, Login, Token, User } from '../types/web.types';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private router: Router) {}

  readonly APIUrl = environment.baseUrls.server + environment.baseUrls.apiUrl;

  token?: string;

  getUserDefaults() {
    return of({
      id: '',
      login: '',
      password: '',
      role: '',
      name: '',
      lastname: '',
      email: '',
      phone: undefined,
      profilePicture: undefined,
      fileMime: undefined,
      description: undefined,
      createdAt: undefined,
      modifiedAt: undefined,
      deleted: undefined,
      banned: undefined,
    } as User);
  }

  getPasswordDefaults() {
    return of({
      oldPassword: '',
      newPassword: '',
    } as ChangePassword);
  }

  login(login: Login): Observable<Token> {
    return this.http.post<Token>(this.APIUrl + 'User/Login', login);
  }
  logout() {
    localStorage.clear();
  }

  getUserById(id: string): Observable<User> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<User>(this.APIUrl + 'User/id/' + id, {
      headers: headers,
    });
  }

  addUser(user: User): Observable<User> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.post<User>(this.APIUrl + 'User', user, {
      headers: headers,
    });
  }

  updateUser(user: User) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.put(this.APIUrl + 'User/' + user.id, user, {
      headers: headers,
    });
  }
  updateAccount(user: User) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.put(this.APIUrl + 'User/Account/' + user.id, user, {
      headers: headers,
    });
  }
  updateAccountPassword(id: string, password: ChangePassword) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.put(
      this.APIUrl + 'User/Account/Password/' + id,
      password,
      {
        headers: headers,
      }
    );
  }

  deleteUser(id: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(this.APIUrl + 'User/' + id, {
      headers: headers,
    });
  }
}
