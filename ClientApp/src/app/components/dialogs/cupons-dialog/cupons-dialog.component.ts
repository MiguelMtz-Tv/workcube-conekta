import { Component, OnInit } from '@angular/core';
import { CuponesService } from 'src/app/services/cupones.service';
import { NgxMaskService } from 'ngx-mask';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-cupons-dialog',
  templateUrl: './cupons-dialog.component.html',
  styleUrls: ['./cupons-dialog.component.css']
})
export class CuponsDialogComponent implements OnInit {

  cupons: any
  emptyCupons: boolean = false

  constructor(
      private cuponesService: CuponesService,
      private spinner: NgxSpinnerService,
    ) { }

  ngOnInit() {
    this.spinner.show("cupons-dialog")
    this.cuponesService.getCupones().subscribe(res => {
      this.spinner.hide("cupons-dialog")
      this.cupons = res
      if(res.length == 0){
        this.emptyCupons = true
      }
    })
  }
}
