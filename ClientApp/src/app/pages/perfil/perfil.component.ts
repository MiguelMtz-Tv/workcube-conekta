import { Component } from '@angular/core';
import { AspNetUserService } from 'src/app/services/asp-net-user.service';
import { FormGroup, FormControl } from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent {

  constructor(private objUSerService: AspNetUserService, private toast: HotToastService){ 
    this.objUSerService.getUserFullName().subscribe(res =>{
      this.form.setValue({
          Nombre: res.nombre || '',
          ApellidoPat: res.apellidoPat || '',
          ApellidoMat: res.apellidoMat || ''
      })
    })
  }

  form = new FormGroup({
    Nombre: new FormControl(),
    ApellidoPat: new FormControl(),
    ApellidoMat: new FormControl(),
  })

  onSubmit(){
    console.log(this.form.value)
    this.objUSerService.updateUser(this.form.value).subscribe(res =>{
      this.toast.success('Usuario actualizado', {
        style: {
          margin: '100px',
        },
        position: 'top-right'
      })
    })
  }


  
}
