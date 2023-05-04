import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent {
  constructor(private dataService: DataService){ }

  form = new FormGroup({
    numTarjeta: new FormControl('', [Validators.required]),
    nombreTitular: new FormControl('', [Validators.required]),
    vencimiento: new FormControl('', [Validators.required]),
    codigo: new FormControl('', [Validators.required])
  })

  onSubmit(){
    this.dataService.setCardDataService(this.form.value)
  }
}
