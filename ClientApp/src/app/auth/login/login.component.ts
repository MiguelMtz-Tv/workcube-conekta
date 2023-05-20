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
  isLoading: boolean = false

  constructor(private auth: AuthService, private router: Router) {
  }

  form = new FormGroup({
    UserName: new FormControl('', [Validators.required, Validators.email]),
    Password: new FormControl('', [Validators.required])
  })

  onSubmit() {
    this.isLoading = true
    this.auth.login(this.form.value).subscribe(
      res => {
        console.log(res)
        this.auth.storeData(res.token, res.id, res.nombreCompleto, res.idCliente)
        this.router.navigate(['/servicios'])
      },
      error => {
        this.isLoading = false
      },
      () => {
        this.isLoading = false
      }
    )
  }

}
