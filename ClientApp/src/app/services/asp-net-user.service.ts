import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';

@Injectable({
  providedIn: 'root'
})
export class AspNetUserService {
  baseUrl: string
  idUser: any
  constructor(private http: HttpClient) { this.baseUrl = getBaseUrl(), this.idUser = localStorage.getItem('Id') }

  getUserFullName(){
    return this.http.get<any>(this.baseUrl+'api/aspnetuser/client/'+this.idUser)
  }

  updateUser(objUser : any){
    return this.http.put<any>(this.baseUrl+'api/aspnetuser/update/'+this.idUser, objUser)
  }
}
