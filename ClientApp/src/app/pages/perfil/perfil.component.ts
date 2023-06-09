import { Component, OnInit } from '@angular/core'
import { AspNetUserService } from 'src/app/services/asp-net-user.service'
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { HotToastService } from '@ngneat/hot-toast'
import { AuthService } from 'src/app/services/auth.service'
import { catchError, throwError } from 'rxjs'
import { NgxSpinnerService } from 'ngx-spinner'
import { Sessions } from 'src/app/applicationConfig/application-service'

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {
  userIsLoading: boolean = false
  passwordIsLoading: boolean = false
  isuserFormDirty: boolean = false;

  passwordIsHidden: boolean = true

  constructor(
    private objUSerService: AspNetUserService, 
    private toast: HotToastService, 
    private auth: AuthService,
    private spinner: NgxSpinnerService,
    ) {}

  ngOnInit(): void {
    this.initialization();
  }

  initialization()
  {
    this.spinner.show()

    let objUser : any = { id : Sessions.getItem("Id") };

    this.objUSerService.getUserFullName(objUser)
    .subscribe(res => {
      if(res.session && res.action)
      {
        this.userForm.setValue({
          Id:           this.auth.getUserId(),
          Nombre:       res?.result.nombre || '',
          ApellidoPat:  res?.result.apellidoPat || '',
          ApellidoMat:  res?.result.apellidoMat || ''
        });
      }
      this.spinner.hide()
    })
  }

  //user form logic
  userForm = new FormGroup({
    Id: new FormControl(this.auth.getUserId()),
    Nombre: new FormControl('', Validators['required']),
    ApellidoPat: new FormControl('', Validators['required']),
    ApellidoMat: new FormControl('', Validators['required']),
  })

  onSubmitUserForm() {
    this.userIsLoading = true
    this.objUSerService.updateUser(this.userForm.value)
      .pipe(
        catchError(error => {
          this.toast.error('No se pudo actualizar el usuario',{
            style: {
              border: '1px solid #FF0000',
              margin: '100px 20px',
              padding: '15px'
            },
            position: 'top-right'
          })
          this.userIsLoading = false
          return throwError(error)
        })
      )
      .subscribe(res => {
        this.toast.success('Usuario actualizado!', {
          style: {
            border: '1px solid #3F51B5',
            margin: '100px 20px',
            padding: '15px'
          },
          iconTheme: {
            primary: '#3F51B5'
          },
          position: 'top-right'
        })
        this.userIsLoading = false
      })
  }

  //password form logic
  setPassword: string = ''
  confirmPassword: string = ''
  canConfirm: boolean = false

  passwordForm = new FormGroup({
    Id: new FormControl(this.auth.getUserId()),
    OldPassword: new FormControl('', Validators['required']),
    NewPassword: new FormControl('', Validators['required']),
    ConfirmPassword: new FormControl('', Validators['required'])
  })

  comparePassword(){
    if(this.setPassword === this.confirmPassword){
      this.canConfirm = true
    }else{
      this.canConfirm = false
    }
  }

  onSubmitPasswordForm(){
    this.passwordIsLoading = true
    this.objUSerService.updatePassword(this.passwordForm.value)
    .subscribe(res => {
      this.passwordIsLoading = false
      if(res.action){
        this.toast.success('Contraseña actualizada!', {
          style: {
            margin: '100px 20px',
            padding: '15px'
          },
          iconTheme: {
            primary: '#3F51B5'
          },
          position: 'top-right'
        })
      }else{
        this.toast.error(res.message,{
          style: {
            border: '1px solid #FF0000',
            margin: '100px 20px',
            padding: '15px'
          },
          position: 'top-right'
        })
      }
      this.userIsLoading = false
    })
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
