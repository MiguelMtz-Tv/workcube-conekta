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
  loginError: boolean = false

  constructor(private auth: AuthService, private router: Router) {
  }

  form = new FormGroup({
    UserName: new FormControl('', [Validators.required]),
    Password: new FormControl('', [Validators.required])
  })

  onSubmit() {
    this.isLoading = true
    this.auth.login(this.form.value).subscribe(
      res => {
        this.auth.storeData(res.token, res.id, res.nombreCompleto, res.idCliente)
        this.router.navigate(['/servicios'])
      },
      error => {
        this.loginError = true
        setTimeout(()=>{
          this.loginError = false
        }, 3000)

        this.isLoading = false
        console.log(error)
      },
      () => {
        this.isLoading = false
      }
    )
  }

}
