import { Component, OnInit } from '@angular/core';
import { CuponesService } from 'src/app/services/cupones.service';

@Component({
  selector: 'app-cupons-dialog',
  templateUrl: './cupons-dialog.component.html',
  styleUrls: ['./cupons-dialog.component.css']
})
export class CuponsDialogComponent implements OnInit {

  cupons: any
  emptyCupons: boolean = false

  constructor(private cuponesService: CuponesService) { }

  ngOnInit() {
    this.cuponesService.getCupones().subscribe(res => {
      this.cupons = res
      if(res.length == 0){
        this.emptyCupons = true
      }
    })
  }
}
