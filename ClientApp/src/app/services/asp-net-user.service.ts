import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { Sessions } from '../applicationConfig/application-service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AspNetUserService {
  baseUrl: string
  idUser: any
  constructor(
    private http: HttpClient,
    private auth: AuthService,
    ) { 
      this.baseUrl = getBaseUrl(), 
      this.idUser = this.auth.getUserId() }

  createNewUser(objUser : any){
    return this.http.post<any>(this.baseUrl+'api/aspnetuser', objUser)
  }

  getUserFullName(){
    return this.http.post<any>(this.baseUrl+'api/aspnetuser/user', {id: this.idUser})
  }

  updateUser(objUser : any){
    return this.http.put<any>(this.baseUrl+'api/aspnetuser/update', objUser) 
  }

  updatePassword(objPass : any){
    return this.http.put<any>(this.baseUrl+'api/aspnetuser/updatepass', objPass)
  }
}
