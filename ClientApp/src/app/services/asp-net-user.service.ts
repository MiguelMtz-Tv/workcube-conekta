import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { Sessions } from '../applicationConfig/application-service';

@Injectable({
  providedIn: 'root'
})
export class AspNetUserService {
  baseUrl: string
  idUser: any
  constructor(private http: HttpClient) { this.baseUrl = getBaseUrl(), this.idUser = localStorage.getItem('Id') }

  createNewUser(objUser : any){
    return this.http.post<any>(this.baseUrl+'api/aspnetuser', objUser)
  }

  getUserFullName(){
    return this.http.post<any>(this.baseUrl+'api/aspnetuser/user', {Id: this.idUser})
  }

  updateUser(objUser : any){
    return this.http.put<any>(this.baseUrl+'api/aspnetuser/update', objUser) 
  }

  updatePassword(objPass : any){
    return this.http.put<any>(this.baseUrl+'api/aspnetuser/updatepass', objPass)
  }
}
