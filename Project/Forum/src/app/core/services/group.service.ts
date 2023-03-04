import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ChangePassword, Login, Token, Group } from '../types/web.types';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  constructor(private http: HttpClient, private router: Router) {}

  readonly APIUrl = environment.baseUrls.server + environment.baseUrls.apiUrl;

  token?: string;

  getGroupDefaults() {
    return of({
      id: '',
      name: '',
      visibility: '',
      iconPicture: '',
      backgroundPicture: undefined,
      description: undefined,
      userId: '',
      createdAt: undefined,
      modifiedAt: undefined,
      deleted: undefined,
      banned: undefined,
    } as Group);
  }

  getGroupList(): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get(this.APIUrl + 'Group', { headers: headers });
  }

  getGroupById(id: string): Observable<Group> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.get<Group>(this.APIUrl + 'Group/id/' + id, {
      headers: headers,
    });
  }

  addGroup(group: Group): Observable<Group> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.post<Group>(this.APIUrl + 'Group', group, {
      headers: headers,
    });
  }

  updateGroup(group: Group) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.put(this.APIUrl + 'Group/' + group.id, group, {
      headers: headers,
    });
  }

  deleteGroup(id: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(this.APIUrl + 'Group/' + id, {
      headers: headers,
    });
  }

  hardDeleteGroup(id: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')!}`,
    });
    return this.http.delete(this.APIUrl + 'Group/' + id + '/hard', {
      headers: headers,
    });
  }
}
