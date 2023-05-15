import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { getBaseUrl } from 'src/main';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  baseUrl: string = getBaseUrl()

  constructor(private http: HttpClient, private auth: AuthService, private router: Router) {
  }

  form = new FormGroup({
    UserName: new FormControl('', [Validators.required, Validators.email]),
    Password: new FormControl('', [Validators.required])
  })

  onSubmit() {
    this.auth.login(this.form.value)
    this.router.navigate(['/servicios'])
  }

}
