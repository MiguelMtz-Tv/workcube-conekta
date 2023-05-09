import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { getBaseUrl } from '../../../main';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private http: HttpClient) {

  }

  createUser() {
    let base: string = getBaseUrl()
    this.http.post(base + 'api/usuario', {
      Cliente: 3,
      IdClient: 1,
      Usuario: 'mikkel',
      Contrasenia: 'mypassword',
      Nombre: 'Azucena',
      ApellidoPat: 'García',
      ApellidoMat: 'Hernandez'
    }).subscribe(res => console.log(res));
  }
}
