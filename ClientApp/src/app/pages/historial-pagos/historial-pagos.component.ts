import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { PagosService } from 'src/app/services/pagos.service';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-historial-pagos',
  templateUrl: './historial-pagos.component.html',
  styleUrls: ['./historial-pagos.component.css']
})
export class HistorialPagosComponent implements OnInit, AfterViewInit{
  id!: number
  dataSource!: MatTableDataSource<any>

  displayedColumns: string[] = ['fecha', 'monto', 'descuento'];

  @ViewChild(MatPaginator) paginator!: MatPaginator
  @ViewChild(MatSort) sort!: MatSort
  constructor(
    private pagosService: PagosService,
    private route: ActivatedRoute
    ) 
    { 
      this.id = Number(this.route.snapshot.paramMap.get('id')) 
    }

  ngOnInit(): void {
    this.pagosService.getPaymentsList(this.id)
      .subscribe(res => {
        this.dataSource = new MatTableDataSource<any>(res)
      })
  }

  ngAfterViewInit() {
    if (this.dataSource) {
      this.dataSource.paginator = this.paginator
      this.dataSource.sort = this.sort
    }
  }
}
