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
import { TarjetasService } from 'src/app/services/tarjetas.service';

@Component({
  selector: 'app-pagar',
  templateUrl: './pagar.component.html',
  styleUrls: ['./pagar.component.css']
})
export class PagarComponent implements OnInit {
  selectedCard: string = 'no seleccionado'

  id: number = 0
  selectedId!: string
  selected: boolean = false

  servicio: any = []
  total: number = 0
  descuento: number = 0
  cuponCode: string = ''

  areDiscount: boolean = false
  
  cuponIsLoading: boolean = false
  paymentIsLoading: boolean = false

 
  constructor(
    private dialog: MatDialog, 
    private tarjetasService: TarjetasService,
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

    this.tarjetasService.listCards()
      .subscribe(res => {
        this.cards = res
        console.log(res)
      })
  }

  cards: any 

  addCard(enterAnimationDuration: string, exitAnimationDuration: string): void{
    this.dialog.open(AddCardComponent,{
      width: '90%',
        maxWidth: '500px',
        minHeight: '300px',
        maxHeight: '1000px',
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

  //Cupon Form management
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
          border: '1px solid green',
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
      console.log({IdServicio: this.id, Codigo: this.cuponCode, CardId: this.selectedId})
    }else{
      console.log({IdServicio: this.id, CardId: this.selectedId})
    }
  }

  selectCard(lastFour: string){
    this.selectedCard = 'Terminada en ' + lastFour
  }
  selectedCardId(id: string){
    this.selectedId = id
    this.selected = true
  }
}
