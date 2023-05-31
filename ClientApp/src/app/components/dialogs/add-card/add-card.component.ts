import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { TarjetasService } from 'src/app/services/tarjetas.service';
import { AuthService } from 'src/app/services/auth.service';
import { loadStripe ,StripeCardElement, StripeElements, Stripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent implements OnInit {
  cardElement!: StripeCardElement;
  stripeElements!: StripeElements;
  stripe!: Stripe;

  constructor(
    private tarjetasService: TarjetasService,
    private auth: AuthService,
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

  onSubmit(){
    this.stripe.createToken(this.cardElement).then(res => {
      if(res.error){
        console.error(res.error)
      }else{
        console.log({
          idClient: parseInt(this.auth.getClientId()!),
          token: res.token.id,
          name: this.form.value.name
        })
        this.tarjetasService.addCard({
          idCliente: parseInt(this.auth.getClientId()!),
          token: res.token.id,
          name: this.form.value.name
        }).subscribe(res => console.log(res))
      }
    })
  }
}
