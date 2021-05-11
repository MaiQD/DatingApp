import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }

  getUserWithRoles(){
    return this.http.get<Partial<User[]>>(this.baseUrl+'admin/users-with-roles');
  }
  updateUserRoles(username, roles){
    return this.http.post(this.baseUrl+'admin/edit-roles/'+username+'?roles='+roles,{})
  }
}
