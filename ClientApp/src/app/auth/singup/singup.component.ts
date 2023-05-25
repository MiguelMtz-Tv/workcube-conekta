import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AspNetUserService } from 'src/app/services/asp-net-user.service';
import { HotToastService } from '@ngneat/hot-toast';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.css']
})
export class SingupComponent {
  baseUrl : string = getBaseUrl() 

  constructor(private http: HttpClient, private aspNetUserService: AspNetUserService, private toast: HotToastService){ }

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
    this.aspNetUserService.createNewUser(this.form.value)
    .pipe(
      catchError(error => { //error.error.text para obtener el mensaje de error
        this.toast.error(error.error.text,{
          style: {
            padding: '15px'
          },
          position: 'bottom-left'
        })
        return throwError(error)
      })
    )
    .subscribe(res => 
      console.log(res)
    )
  }
  
}
