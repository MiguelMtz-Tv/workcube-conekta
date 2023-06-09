import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import { AddCardComponent } from 'src/app/components/dialogs/add-card/add-card.component';
import { ConfirmarPagoComponent } from 'src/app/components/dialogs/confirmar-pago/confirmar-pago.component';
import { ActivatedRoute } from '@angular/router';
import { ServiciosService } from 'src/app/services/servicios.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CuponesService } from 'src/app/services/cupones.service';
import { catchError, throwError } from 'rxjs';
import { HotToastService } from '@ngneat/hot-toast';
import { TarjetasService } from 'src/app/services/tarjetas.service';
import { PagosService } from 'src/app/services/pagos.service';
import { AuthService } from 'src/app/services/auth.service';
import { DataService } from 'src/app/services/data.service';
import { Router } from '@angular/router';
import { Action } from 'rxjs/internal/scheduler/Action';

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

  arePaymentMethods!: boolean

  servicio: any = []
  total: number = 0
  descuento: number = 0
  cuponCode: string = ''
  idCupon!: number

  areCupon: boolean = false
  
  cuponIsLoading: boolean = false
  paymentIsLoading: boolean = false

  cards: any 

  constructor(
    private dialog: MatDialog, 
    private tarjetasService: TarjetasService,
    private route: ActivatedRoute, 
    private serviciosService: ServiciosService,
    private cuponesService: CuponesService,
    private toast: HotToastService,
    private pagosService: PagosService,
    private auth: AuthService,
    private dataService: DataService,
    private router: Router,
    )
    { 
    this.id = Number(this.route.snapshot.paramMap.get('id')) 
    }

  getCards(){
    this.tarjetasService.listCards()
      .subscribe(res => {
        if(res.action){
          res.result.length == 0 ? this.arePaymentMethods = false : this.arePaymentMethods = true 
          this.cards = res.result
        }else{
          console.error('No se pudieron recuperar los metodos de pago')
        }
      })
  }

  ngOnInit() {
    this.serviciosService.getServiceDetails(this.id).subscribe(res => {
      if(res.action && res.session){
        this.servicio = res.result
      }else{
        this.router.navigateByUrl('/servicios')
      }
      this.total = res.result.servicioTipoCosto
    })
    //obtener tarjetas de la api de stripe
    this.getCards()
    //observamos cuando se añade una nueva tarjeta
    this.dataService.observeData().subscribe(data => {
      this.getCards()
    })
  }

  addCard(enterAnimationDuration: string, exitAnimationDuration: string): void{
    this.dialog.open(AddCardComponent,{
      width: '90%',
        maxWidth:   '500px',
        minHeight:  '300px',
        maxHeight:  '1000px',
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
    this.cuponesService.getCupon(this.cuponForm.value.code, this.id)
    .subscribe(res => {
      if(res.action){
        this.idCupon =      res.result.idCupon,
        this.descuento =    res.result.monto
        this.cuponCode =    res.result.codigo
        this.areCupon =     true
        this.total =        this.servicio.servicioTipoCosto - res.result.monto

        this.toast.success('Se aplicó un cupón de descuento',{
          style: {
            border:     '1px solid green',
            margin:     '100px 20px',
            padding:    '15px'
          },
          position:     'top-right'
        })
        this.cuponIsLoading = false 
      }else{
        this.toast.error('Cupón invalido: '+res.message,{
          style: {
            border:     '1px solid red',
            margin:     '100px 20px',
            padding:    '15px'
          },
          position:     'top-right'
        })
        this.cuponIsLoading = false
      }
    })
  }

  confirmPayment(enterAnimationDuration: string, exitAnimationDuration: string){
    this.dialog.open(ConfirmarPagoComponent,{
      width:                    '90%',
      maxWidth:                 '500px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: {
        IdAspNetUser: this.auth.getUserId(),
        IdCliente:    this.auth.getClientId(),
        IdServicio:   this.id, 
        IdCard:       this.selectedId,
        IdCupon:      this.idCupon,
        areCupon:     this.areCupon,
      }
    })
  } 

  selectCard(lastFour: string){
    this.selectedCard = 'Terminada en ' + lastFour
  }
  selectedCardId(id: string){
    this.selectedId = id
    this.selected =   true
  }
}
