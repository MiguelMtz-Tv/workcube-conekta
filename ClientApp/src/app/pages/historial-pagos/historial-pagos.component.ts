import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { PagosService } from 'src/app/services/pagos.service';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ServiciosService } from 'src/app/services/servicios.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatDialog } from '@angular/material/dialog';
import { ViewPdfPaymentComponent } from 'src/app/components/dialgos/view-pdf-payment/view-pdf-payment.component';

@Component({
  selector: 'app-historial-pagos',
  templateUrl: './historial-pagos.component.html',
  styleUrls: ['./historial-pagos.component.css']
})
export class HistorialPagosComponent implements OnInit, AfterViewInit{
  id!: number
  dataSource!: MatTableDataSource<any>

  displayedColumns: string[] = ['folio','fecha', 'monto', 'descuento', 'total', 'pago', 'imprimir'];
  service: any

  isEmpty: boolean = false

  @ViewChild(MatPaginator) paginator!: MatPaginator
  @ViewChild(MatSort) sort!: MatSort

  constructor(
    private pagosService: PagosService,
    private route: ActivatedRoute,
    private serviciosService: ServiciosService,
    private spinner: NgxSpinnerService,
    private dialgo: MatDialog,
    ) 
    { 
      this.id = Number(this.route.snapshot.paramMap.get('id'))
    }

  ngOnInit(): void {
    this.spinner.show('historial-pagos')
    this.pagosService.getPaymentsList(this.id)
      .subscribe(res => {
        if (res.action && res.session){
          this.dataSource = new MatTableDataSource<any>(res.result)
          this.spinner.hide('historial-pagos')
          if(res.result.length == 0){
            this.isEmpty = true
          }else{
            this.isEmpty = false
          }
        }else{
          this.isEmpty = true;
        }
      })

    this.serviciosService.getServiceDetails(this.id)
      .subscribe(res => {
        this.service = res.result
      })
  }

  ngAfterViewInit() {
    if (this.dataSource) {
      this.dataSource.paginator = this.paginator
      this.dataSource.sort = this.sort
    }
  }

  openPdf(idPago : number){
    this.dialgo.open(ViewPdfPaymentComponent, {
      data: idPago,
      width: '1024px',
      height:   '859.5px',
      enterAnimationDuration: '150ms',
      exitAnimationDuration: '150ms',
    })
  }
}
