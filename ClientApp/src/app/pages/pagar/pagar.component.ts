import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import { AddCardComponent } from 'src/app/components/dialogs/add-card/add-card.component';
import { ConfirmarPagoComponent } from 'src/app/components/dialogs/confirmar-pago/confirmar-pago.component';
import { DataService } from 'src/app/services/data.service';
import { ActivatedRoute } from '@angular/router';
import { ServiciosService } from 'src/app/services/servicios.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CuponesService } from 'src/app/services/cupones.service';
import { catchError, throwError } from 'rxjs';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-pagar',
  templateUrl: './pagar.component.html',
  styleUrls: ['./pagar.component.css']
})
export class PagarComponent implements OnInit {
  selectedCard: string = '5771'

  id: number = 0

  servicio: any = []
  total: number = 0
  descuento: number = 0
  cuponCode: string = ''

  areDiscount: boolean = false
  
  cuponIsLoading: boolean = false
  paymentIsLoading: boolean = false

 
  constructor(
    private dialog: MatDialog, 
    private dataService: DataService, 
    private route: ActivatedRoute, 
    private serviciosService: ServiciosService,
    private cuponesService: CuponesService,
    private toast: HotToastService
    )
    { 
    this.id = Number(this.route.snapshot.paramMap.get('id')) 
    }

  ngOnInit() {
    this.serviciosService.getServiceDetails(this.id).subscribe(res => {
      this.servicio = res
      this.total = res.servicioTipoCosto
    })

    this.dataService.getCardDataService().subscribe((form)=>{
      let newId=          this.cards.length + 1
      let lastFour =      form.numTarjeta.slice(form.numTarjeta.length-4)
      let data = {
        id: newId,
        finishedIn:       form.numTarjeta,
        owner:            form.nombreTitular,
        expiration:       form.vencimiento,
        lastFour:         lastFour,
      }
      this.cards.push(data);
    })
  }

  cards: any = [
    {
      id:         '1',
      finishedIn: '4152313451205771',
      owner:      'Miguel Martinez Castro',
      expiration: '07/24',
      lastFour:   '5771',
    },
    {
      id:         '2',
      finishedIn: '4152313451204489',
      owner:      'Carlos Rivera Lopez',
      expiration: '07/23',
      lastFour:   '4489',
    },
    {
      id:         '3',
      finishedIn: '4152313451202213',
      owner:      'Martín Mendez Loeza',
      expiration: '07/21',
      lastFour:   '2213',
    }
  ]

  addCard(enterAnimationDuration: string, exitAnimationDuration: string): void{
    this.dialog.open(AddCardComponent,{
      width:                    '90%',
      maxWidth:                 '700px',
      enterAnimationDuration,
      exitAnimationDuration
    })
  }
  confirmPayment(enterAnimationDuration: string, exitAnimationDuration: string): void{
    this.dialog.open(ConfirmarPagoComponent,{
      width:                    '90%',
      maxWidth:                 '500px',
      enterAnimationDuration,
      exitAnimationDuration
    })
  } 

  cuponForm = new FormGroup({
    code: new FormControl('', Validators['required'])
  })

  onSubmitCuponForm(){
    this.cuponIsLoading = true
    this.cuponesService.getCupon(this.cuponForm.value.code)
    .pipe(
      catchError(error => {
        this.toast.error('Cupón invalido',{
          style: {
            border: '1px solid red',
            margin:     '100px 20px',
            padding:    '15px'
          },
          position:     'top-right'
        })
        this.cuponIsLoading = false
        return throwError(error)
      })
    )
    .subscribe(res => {
      this.descuento = res.monto
      this.cuponCode = res.codigo
      this.areDiscount = true
      this.total = this.servicio.servicioTipoCosto - res.monto
      this.toast.success('Se aplicó un cupón de descuento',{
        style: {
          margin:     '100px 20px',
          padding:    '15px'
        },
        position:     'top-right'
      })
      this.cuponIsLoading = false 
    })
  }

  onSubmitPayment(){
    if(this.areDiscount){
      console.log({IdServicio: this.id, Codigo: this.cuponCode})
    }else{
      console.log(this.id)
    }
  }

  selectCard(lastFour: string){
    this.selectedCard = lastFour
  }
}
