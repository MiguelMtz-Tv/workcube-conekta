import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Server } from 'src/app/libraries/server';
import { NgxSpinnerService } from 'ngx-spinner';
import { PagosService } from 'src/app/services/pagos.service';

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
    private pagosService: PagosService,
  ){}

  ngOnInit() {
    this.spinner.show('view-pdf')
    this.pagosService.getPdfFile(this.data).
      subscribe((res: Blob) => {
        this.pdfSrc = URL.createObjectURL(res)
        this.spinner.hide('view-pdf')
      })
  }

  rendered(){
    this.spinner.hide('view-pdf')
  }
}