import { Component, OnInit } from '@angular/core';
import { PagosService } from 'src/app/services/pagos.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-historial-pagos',
  templateUrl: './historial-pagos.component.html',
  styleUrls: ['./historial-pagos.component.css']
})
export class HistorialPagosComponent implements OnInit{
  id!: number
  payments: any = []

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
        this.payments = res
      })
  }
}
