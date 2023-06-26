import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';
import { MatDialog } from '@angular/material/dialog';
import { AddCardComponent } from 'src/app/components/dialogs/add-card/add-card.component';
import { TarjetasService } from 'src/app/services/tarjetas.service';
import { DataService } from 'src/app/services/data.service';
import { NgxSpinnerService } from 'ngx-spinner';

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
    private spinner: NgxSpinnerService,
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
    this.spinner.show('tarjetas')
    this.tarjetasService.listCards()
    .subscribe(
      res => {
      this.spinner.hide('tarjetas')
      this.cards = res.result
      },
      error => {
        console.log(error)
        this.spinner.hide()
      }
    )
  }

  ngOnInit(): void {
    this.getCards()
    //observamos cada que se añada una tarjeta
    this.dataService.data$.subscribe(data => {
      this.getCards()
      this.toast.success(data, {
        style: {
          border: '1px solid #3F51B5',
          margin: '100px 20px',
          padding: '15px'
        },
        iconTheme: {
          primary: '#3F51B5'
        },
        position: 'top-right'
      });
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
