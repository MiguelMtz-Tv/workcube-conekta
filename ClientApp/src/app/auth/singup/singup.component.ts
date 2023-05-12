import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';

@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.css']
})
export class SingupComponent {
  baseUrl : string = getBaseUrl() 
  constructor(private http: HttpClient){ }

  addUser(){
    this.http.post(`${this.baseUrl}api/AspNetUser`,{
      IdCliente: 1,
      UserName: 'usuario1',
      Password: 'password', 
      Nombre: 'Miguel', 
      ApellidoPat: 'Martinez', 
      ApellidoMat: 'Castro', 
      Email: 'usuario1@email.com'
    }).subscribe(res => console.log(res))
  }
  
}
