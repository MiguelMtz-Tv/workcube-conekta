import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { HotToastService } from '@ngneat/hot-toast';
import { ServiciosService } from 'src/app/services/servicios.service';


@Component({
  selector: 'app-servicios',
  templateUrl: './servicios.component.html',
  styleUrls: ['./servicios.component.css']
})
export class ServiciosComponent implements OnInit {
  services: any

  constructor(private dataService: DataService, private toast: HotToastService, private serviciosService: ServiciosService){
    this.serviciosService.getUserServices().subscribe(res => {
      this.services = res
      console.log(res)
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
