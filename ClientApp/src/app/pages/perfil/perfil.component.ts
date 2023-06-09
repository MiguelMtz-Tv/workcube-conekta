import { Component, OnInit } from '@angular/core'
import { AspNetUserService } from 'src/app/services/asp-net-user.service'
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { HotToastService } from '@ngneat/hot-toast'
import { AuthService } from 'src/app/services/auth.service'
import { catchError, throwError } from 'rxjs'
import { NgxSpinnerService } from 'ngx-spinner'



@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {
  userIsLoading: boolean = false
  passwordIsLoading: boolean = false
  isuserFormDirty: boolean = false;

  constructor(
    private objUSerService: AspNetUserService, 
    private toast: HotToastService, 
    private auth: AuthService,
    private spinner: NgxSpinnerService,
    ) {}

  ngOnInit(): void {
    this.spinner.show()
    this.objUSerService.getUserFullName().subscribe(res => {
      this.userForm.setValue({
        Id: this.auth.getUserId(),
        Nombre: res.nombre || '',
        ApellidoPat: res.apellidoPat || '',
        ApellidoMat: res.apellidoMat || ''
      })
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
    .pipe(
      catchError(error => {
        this.passwordIsLoading = false
        this.toast.error('Contraseña incorrecta',{
          style: {
            border: '1px solid #FF0000',
            margin: '100px 20px',
            padding: '15px'
          },
          position: 'top-right'
        })
        return throwError(error)
      })
    )
    .subscribe(res => {
      this.passwordIsLoading = false
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
      this.userIsLoading = false
      console.log(res)
    })
  }
}
