import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { getBaseUrl } from 'src/main';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  baseUrl: string = getBaseUrl()

  constructor(private http: HttpClient, private auth: AuthService) {
  }

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  })

  onSubmit(){
    this.http.post(`${this.baseUrl}api/usuario/login`, {
      Usuario: this.form.value.email,
      Contrasenia: this.form.value.password
    }).subscribe(res => console.log(res))
  }

  getUser(){
    this.http.post(`${this.baseUrl}api/usuario/login`, {
      Usuario: 'usuario2',
      Contrasenia: '1234'
    }).subscribe(res => console.log(res))
  }

}
