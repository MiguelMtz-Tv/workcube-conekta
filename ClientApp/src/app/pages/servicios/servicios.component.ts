import { Component, OnInit } from '@angular/core';
import { ServiciosService } from 'src/app/services/servicios.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-servicios',
  templateUrl: './servicios.component.html',
  styleUrls: ['./servicios.component.css']
})
export class ServiciosComponent implements OnInit {
  services: any
  emptyServices: boolean = false
  areServices: boolean = false

  constructor(
    private serviciosService: ServiciosService,
    private spinner: NgxSpinnerService,  
    private dataService: DataService,
  ) {}

  getServices(){
    this.spinner.show()
    this.serviciosService.getUserServices().subscribe(res => {
      this.spinner.hide()
      if(res.result.length == 0){
        this.emptyServices = true
      }else{
        console.log(res.result)
        this.services = res.result
        this.areServices = true
      }
    });
  }

  ngOnInit(): void {
    this.getServices()
    //observamos el cambio de un servicio
    this.dataService.data$.subscribe(event => this.getServices())
  }

}
