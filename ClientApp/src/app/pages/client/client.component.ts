import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ClienteService } from 'src/app/services/cliente.service';
import { HotToastService } from '@ngneat/hot-toast';
import { Router } from '@angular/router';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent {
  isLoading: boolean = false

  form =              new FormGroup({
    RFC:              new FormControl('', Validators['required']),
    RazonSocial:      new FormControl('', Validators['required']),
    NombreComercial:  new FormControl('', Validators['required']),
    NumeroContrato:   new FormControl('', Validators['required']),
    Telefono:         new FormControl('', Validators['required']),
    Correo:           new FormControl('', Validators['required']),
    Direccion:        new FormControl('', Validators['required']),
    CodigoPostal:     new FormControl('', Validators['required']),
    Code:             new FormControl('', Validators['required']),
  })

  constructor(
    private clienteService: ClienteService,
    private toast: HotToastService,
    private route: Router,
    ){ }

  onSubmit(){
    this.isLoading = true
    this.clienteService.addClient(this.form.value).subscribe( 
      res => {
        console.log(res)
        this.isLoading = false
        this.route.navigateByUrl('/registro');
      },
      error => {
        this.isLoading = false
        this.toast.error(error)
      }
    )
  }
}
