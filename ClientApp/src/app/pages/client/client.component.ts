import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ClienteService } from 'src/app/services/cliente.service';
import { HotToastService } from '@ngneat/hot-toast';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent {
  isLoading: boolean = false
  pdfSrc!: string
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
    private toast:          HotToastService,
    private route:          Router,
    private dialog:         MatDialog,
    private http:           HttpClient,
    ){}

  onSubmit(){
    this.isLoading = true
    this.clienteService.addClient(this.form.value).subscribe( 
      res => {
        if(res.action){
          this.isLoading = false
          this.route.navigateByUrl('/registro')
          this.isLoading = false
        }else{
          console.log({Title: res.title, Message: res.message})
          this.isLoading = false
        }
      }
    )
  }

  viewPdf(){
    this.pdfSrc = "https://localhost:44484/api/pagos/file"
  }
}