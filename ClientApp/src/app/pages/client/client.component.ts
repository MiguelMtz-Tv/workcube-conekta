import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ClienteService } from 'src/app/services/cliente.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent {
  form = new FormGroup({
    RFC: new FormControl('', Validators['required']),
    RazonSocial: new FormControl('', Validators['required']),
    NombreComercial: new FormControl('', Validators['required']),
    NumeroContrato: new FormControl('', Validators['required']),
    Telefono: new FormControl('', Validators['required']),
    Correo: new FormControl('', Validators['required']),
    Direccion: new FormControl('', Validators['required']),
    CodigoPostal: new FormControl('', Validators['required']),
    Code: new FormControl('', Validators['required']),
  })

  constructor(private clienteService: ClienteService){ }

  onSubmit(){
    this.clienteService.addClient(this.form.value).subscribe( res => console.log(res))
    console.log(this.form.value)
  }
}
