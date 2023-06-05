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
  areServices: boolean = false

  constructor(private serviciosService: ServiciosService){
    this.serviciosService.getUserServices().subscribe(res => {
      if(res.length == 0){
        this.emptyServices = true 
      }else{
        this.services = res
        this.areServices = true
      }
    });
  }

  ngOnInit(): void {

  }

}
