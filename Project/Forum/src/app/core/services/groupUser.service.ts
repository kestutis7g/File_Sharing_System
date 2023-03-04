import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ChangePassword, Login, Token, GroupUser } from '../types/web.types';

@Injectable({
  providedIn: 'root',
})
export class GroupUserService {
  constructor(private http: HttpClient, private router: Router) {}

  readonly APIUrl = environment.baseUrls.server + environment.baseUrls.apiUrl;

  token?: string;

  getGroupUserByIds(groupId: string, userId: string): Observable<GroupUser> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<GroupUser>(
      this.APIUrl + 'GroupUser/' + groupId + '/' + userId,
      {
        headers: headers,
      }
    );
  }
  getGroupUsersByGroupId(groupId: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<GroupUser>(
      this.APIUrl + 'GroupUser/groupId/' + groupId,
      {
        headers: headers,
      }
    );
  }
  getGroupsByUserId(userId: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<GroupUser>(
      this.APIUrl + 'GroupUser/userId/' + userId,
      {
        headers: headers,
      }
    );
  }

  addGroupUser(groupUser: GroupUser): Observable<GroupUser> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.post<GroupUser>(this.APIUrl + 'GroupUser', groupUser, {
      headers: headers,
    });
  }

  deleteGroupUser(groupId: string, userId: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(
      this.APIUrl + 'GroupUser/' + groupId + '/' + userId,
      {
        headers: headers,
      }
    );
  }
}
