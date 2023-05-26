import { Component, OnInit } from '@angular/core';
import { ServiciosService } from 'src/app/services/servicios.service';

@Component({
  selector: 'app-servicios',
  templateUrl: './servicios.component.html',
  styleUrls: ['./servicios.component.css']
})
export class ServiciosComponent implements OnInit {
  services: any
  emptyServices: boolean = false

  constructor(private serviciosService: ServiciosService){
    this.serviciosService.getUserServices().subscribe(res => {
      this.services = res
      if(res.length == 0){
        this.emptyServices = true 
      }
    });
  }

  ngOnInit(): void {
    /* var objST = this.services.filter(x => x.IdServicioTipo == id);
    var objStore = {
      idServicio : 1,
      IdServicioTipo : 1,
      objST.Name : "Serv1",
      objST.Descripcion : ""
    }; */
  }

}
