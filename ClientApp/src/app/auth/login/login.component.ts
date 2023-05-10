import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { getBaseUrl } from 'src/main';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  baseUrl : string = getBaseUrl()
  constructor(private http: HttpClient) {}

  getUser(){
    this.http.post(`${this.baseUrl}api/usuario/login`, {
      Usuario: 'usuario2',
      Contrasenia: '1234'
    }).subscribe(res => console.log(res))
  }

}
