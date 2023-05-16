import { Component } from '@angular/core';
import { AspNetUserService } from 'src/app/services/asp-net-user.service';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent {
  userData: any

  constructor(private objUSerService: AspNetUserService){ 
    this.objUSerService.getUserFullName().subscribe(res => {
      this.setUserData(res)
    })
  }

  //get names
  setUserData(res : any){
    this.userData = res
  }

  form = new FormGroup({
    Nombre: new FormControl(''),
    ApellidoPat: new FormControl(''),
    ApellidoMat: new FormControl(''),
  })

  onSubmit(){
    console.log(this.form.value)
    this.objUSerService.updateUser(this.form.value).subscribe(res => res)
  }


  
}
