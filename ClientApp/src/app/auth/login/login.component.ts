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

  passwordIsHidden: boolean = true

  errorMssg!: string

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
        this.auth.storeData(
          res.token, 
          res.id, 
          res.nombreCompleto, 
          res.idCliente,
          res.email,
          res.stripePK,
          )
        this.router.navigate(['/servicios'])
        this.isLoading = false
      },
      error => {
        this.errorMssg = error.error
        this.loginError = true
        setTimeout(()=>{
          this.loginError = false
        }, 3000)
        
        this.isLoading = false
      },
      () => {
        this.isLoading = false
      }
    )
  }

  toggleHiddenPass(){
    let passFields = document.getElementsByClassName('pass-field')
    let icon = document.getElementById('pass-icon') 

    if(this.passwordIsHidden){
      this.passwordIsHidden = false
      icon?.setAttribute('fontIcon', 'visibility')

      for(let i = 0; i < passFields.length; i++){
        passFields[i].setAttribute('type', 'text')
      }
    }else{
      this.passwordIsHidden = true
      icon?.setAttribute('fontIcon', 'visibility_off')

      for(let i = 0; i < passFields.length; i++){
        passFields[i].setAttribute('type', 'password')
      }
    }
  }

}
