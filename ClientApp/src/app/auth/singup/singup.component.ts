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
    this.http.post(`${this.baseUrl}api/usuario`,{
      IdCliente: 5,
      Usuario: 'usuario5',
      Contrasenia: 'mypassword', 
      Nombre: 'Sheila', 
      ApellidoPat: 'Rodriguez', 
      ApellidoMat: 'Leyva', 
    }).subscribe(res => console.log(res))
  }
  
}
