import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { DataService } from 'src/app/services/data.service';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-tarjetas',
  templateUrl: './tarjetas.component.html',
  styleUrls: ['./tarjetas.component.css']
})
export class TarjetasComponent implements OnInit {
  constructor(private dataService: DataService, private toast: HotToastService){}

  cards = [
    {
      id: '1',
      finishedIn: '4152313451205771',
      owner: 'Miguel Martinez Castro',
      expiration: '07/24',
      lastFour: '5771',
    },
    {
      id: '2',
      finishedIn: '4152313451204489',
      owner: 'Carlos Rivera Lopez',
      expiration: '07/23',
      lastFour: '4489',
    },
    {
      id: '3',
      finishedIn: '4152313451202213',
      owner: 'Martín Mendez Loeza',
      expiration: '07/21',
      lastFour: '2213',
    }
  ];

  //form management

  form = new FormGroup({
    numTarjeta: new FormControl('', [Validators.required]),
    nombreTitular: new FormControl('', [Validators.required]),
    vencimiento: new FormControl('', [Validators.required]),
    codigo: new FormControl('', [Validators.required])
  })

  //close modal
  closeModal(){
    return this.dataService.setModalClose('close');
  }

  onSubmit(){
    let number = this.cards.length + 1
    let lastFour = this.form.value['numTarjeta']?.slice(this.form.value['numTarjeta']?.length - 4 )
    let data = {
      id:  number.toString() || '',
      finishedIn: this.form.value['numTarjeta'] || '',
      owner: this.form.value['nombreTitular'] || '',
      expiration: this.form.value['vencimiento'] || '',
      lastFour: lastFour?.toString() || '',
    }
    this.cards.push(data)
    this.closeModal()
    this.toast.success('Tarjeta añadida',{
      style: {
        margin: '90px'
      }
    })
  }

  ngOnInit(): void {
    //filter or "delete" cards
    this.dataService.getPaymentCardId().subscribe((id)=>{
      this.cards = this.cards.filter((object) => object.id != id)
    })

    //edit a card
    this.dataService.getPaymentCardNewData().subscribe((data) => {
      let editIndex = this.cards.findIndex((obj) => obj.id === data.id)
      this.cards[editIndex].expiration = data.expiration
    })
  }
}
