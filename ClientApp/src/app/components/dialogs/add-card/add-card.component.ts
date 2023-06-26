import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { TarjetasService } from 'src/app/services/tarjetas.service';
import { AuthService } from 'src/app/services/auth.service';
import { loadStripe ,StripeCardElement, StripeElements, Stripe } from '@stripe/stripe-js';
import { HotToastService } from '@ngneat/hot-toast';
import { DataService } from 'src/app/services/data.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent implements OnInit {
  cardElement!: StripeCardElement
  stripeElements!: StripeElements
  stripe!: Stripe

  isLoading: boolean = false

  constructor(
    private tarjetasService: TarjetasService,
    private auth: AuthService,
    private toast: HotToastService,
    private data: DataService,
    private dialogRef: MatDialogRef<AddCardComponent>,
    ){ }

  ngOnInit() {
    const stripePromise = loadStripe('pk_test_51NDAKSHo0a7qOJb87jobsvr8AyT9MVHgf3a4vzvhkDLZIHuOUDnpYATHh7tkR2vQftqJBFkwJsvrxQuDOYGs4qyE00zIK6GN26');
    stripePromise.then((stripe) => {
      this.stripe = stripe!;
      this.stripeElements = stripe!.elements()
      this.cardElement = this.stripeElements.create('card',{
        style: {
          base: { 
            fontWeight: '300',
            fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
            fontSize: '16px',
            '::placeholder': {
              color: '#CFD7E0'
            }
          }
        }
      })
      this.cardElement.mount('#card-element'); 
    })
  }
  
  form = new FormGroup({
    name: new FormControl('', [Validators.required])
  })

  errorToast(message: string){
    return this.toast.error(message, {
      style: {
        border: '1px solid #FF0000',
        margin: '100px 20px',
        padding: '15px'
      },
      position: 'top-right'
    })
  }

  onSubmit(){
    this.isLoading = true
    this.stripe.createToken(this.cardElement).then(res => {
      if(res.error){
        this.errorToast(res.error.message || '')
        this.isLoading = false
      }else{
        this.tarjetasService.addCard({
          idCliente: parseInt(this.auth.getClientId()!),
          token: res.token.id,
          name: this.form.value.name
        })
        .subscribe(
          res => {
            if(res.action){
              this.data.updateData('Tarjeta añadida')
              this.dialogRef.close()
            }else{
              this.isLoading = false
              this.errorToast('Esta tarjeta no es valida o tiene fondos insuficientes.')
            }
          }
        )
      }
    })
  }
}
