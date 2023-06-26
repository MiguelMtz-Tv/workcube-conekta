import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Server } from 'src/app/libraries/server';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-view-pdf-payment',
  templateUrl: './view-pdf-payment.component.html',
  styleUrls: ['./view-pdf-payment.component.css']
})
export class ViewPdfPaymentComponent implements OnInit {
  loading: boolean = true
  pdfSrc!: string

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private spinner: NgxSpinnerService,
  ){
    
  }

  ngOnInit() {
    this.spinner.show('view-pdf')
    this.pdfSrc = Server.base()+'api/pagos/file/'+this.data
  }

  rendered(){
    this.spinner.hide('view-pdf')
  }
}