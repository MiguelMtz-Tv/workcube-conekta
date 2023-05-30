import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { DataService } from 'src/app/services/data.service';
import { TarjetasService } from 'src/app/services/tarjetas.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent {
  constructor(
    private dataService: DataService, 
    private tarjetasService: TarjetasService, 
    private auth: AuthService,
    ){ }

  form = new FormGroup({
    number: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
    exp: new FormControl('', [Validators.required]),
    cvc_check: new FormControl('', [Validators.required])
  })

  onSubmit(){
    let exp_month = this.form.value.exp?.substring(0,2)
    let exp_year = this.form.value.exp?.substring(2)

    let cardData = {
      IdCliente: this.auth.getClientId(),
      number: this.form.value.number,
      name: this.form.value.name,
      exp_month: exp_month,
      exp_year: exp_year,
      cvc_check: this.form.value.cvc_check,
    }
    console.log(cardData)
    this.tarjetasService.addCard(cardData).subscribe(res => console.log(res))
  }
}
