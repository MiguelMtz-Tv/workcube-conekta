import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';
import { MatDialog } from '@angular/material/dialog';
import { AddCardComponent } from 'src/app/components/dialogs/add-card/add-card.component';
import { TarjetasService } from 'src/app/services/tarjetas.service';

@Component({
  selector: 'app-tarjetas',
  templateUrl: './tarjetas.component.html',
  styleUrls: ['./tarjetas.component.css']
})
export class TarjetasComponent implements OnInit {
  constructor(
    private toast: HotToastService, 
    private dialod: MatDialog,
    private tarjetasService: TarjetasService,
    ){}

  cards : any

//form management
  form = new FormGroup({
    numTarjeta: new FormControl('', [Validators.required]),
    nombreTitular: new FormControl('', [Validators.required]),
    vencimiento: new FormControl('', [Validators.required]),
    codigo: new FormControl('', [Validators.required])
  })

  ngOnInit(): void {
    this.tarjetasService.listCards()
    .subscribe(res => {
      this.cards = res
    })
  }

  openAddCardModal(enterAnimations: string, exitAnimation: string){
    this.dialod.open(AddCardComponent, {
      width: '90%',
        maxWidth: '500px',
        minHeight: '300px',
        maxHeight: '1000px',
        enterAnimationDuration: enterAnimations,
        exitAnimationDuration: exitAnimation,
    })
  }
}
