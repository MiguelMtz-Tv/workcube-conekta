import { Component, OnInit } from '@angular/core';
import { ServiciosService } from 'src/app/services/servicios.service';
import { NgxSpinnerService } from 'ngx-spinner';

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
    ){
  }

  ngOnInit(): void {
    this.spinner.show()
    this.serviciosService.getUserServices().subscribe(res => {
      this.spinner.hide()
      if(res.length == 0){
        this.emptyServices = true
      }else{
        this.services = res
        this.areServices = true
      }
    });
  }

}
