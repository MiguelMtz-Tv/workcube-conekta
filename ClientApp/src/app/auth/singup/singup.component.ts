import { Component } from '@angular/core';
import { getBaseUrl } from 'src/main';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AspNetUserService } from 'src/app/services/asp-net-user.service';
import { catchError, throwError } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.css']
})
export class SingupComponent {
  baseUrl : string = getBaseUrl()
   
  singupError: boolean = false
  singupErrorMessage: string = ''

  isLoading: boolean = false
  

  constructor(private aspNetUserService: AspNetUserService, private auth: AuthService, private router: Router){ }

  form = new FormGroup({
    Nombre:               new FormControl('', Validators['required']),
    ApellidoPat:          new FormControl('', Validators['required']),
    ApellidoMat:          new FormControl('', Validators['required']),
    UserName:             new FormControl('', Validators['required']),
    Email:                new FormControl('', Validators['required']),
    NumeroContrato:       new FormControl('', Validators['required']),
    Password:             new FormControl('', Validators['required']),
    passConfirm:          new FormControl('', Validators['required'])
  })

  //password form logic
  setPassword: string = ''
  confirmPassword: string = ''
  canConfirm: boolean = false

  comparePassword(){
    if(this.setPassword === this.confirmPassword){
      this.canConfirm = true
      console.log('match')
    }else{
      this.canConfirm = false
    }
  }

  addUser(){
    this.isLoading = true
    this.aspNetUserService.createNewUser(this.form.value)
    .subscribe(res =>{
        this.isLoading = false
        console.log(res)
        if(res.action){
          this.auth.login({
            UserName: this.form.value.UserName,
            Password: this.form.value.Password
          }).subscribe(
            res => {
              this.auth.storeData(
                res.token, 
                res.id, 
                res.nombreCompleto, 
                res.idCliente,
                res.email,
                res.stripePk
              )
              this.router.navigate(['/servicios'])
              this.isLoading = false
            })
        }else{
          this.singupError = true
          this.singupErrorMessage = res.message
          console.log()
          setTimeout(() => {
            this.singupError = false
          }, 3000)
        }
      }
    )
  }
  
}
