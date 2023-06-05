import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';
import { MatDialog } from '@angular/material/dialog';
import { AddCardComponent } from 'src/app/components/dialogs/add-card/add-card.component';
import { TarjetasService } from 'src/app/services/tarjetas.service';
import { DataService } from 'src/app/services/data.service';

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
    private dataService: DataService,
    ){}

  cards : any

//form management
  form = new FormGroup({
    numTarjeta: new FormControl('', [Validators.required]),
    nombreTitular: new FormControl('', [Validators.required]),
    vencimiento: new FormControl('', [Validators.required]),
    codigo: new FormControl('', [Validators.required])
  })

  getCards(){
    this.tarjetasService.listCards()
    .subscribe(res => {
      this.cards = res
    })
  }

  ngOnInit(): void {
    this.getCards()
    //observamos cada que se añada una tarjeta
    this.dataService.data$.subscribe(data => this.getCards())
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
