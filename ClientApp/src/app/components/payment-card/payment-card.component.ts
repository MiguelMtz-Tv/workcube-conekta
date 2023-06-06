import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';
import { MatDialog } from '@angular/material/dialog';
import { DeleteCardComponent } from '../dialogs/delete-card/delete-card.component';
import { AuthService } from 'src/app/services/auth.service';
import { UpdateCardComponent } from '../dialogs/update-card/update-card.component';

@Component({
  selector: 'app-payment-card',
  templateUrl: './payment-card.component.html',
  styleUrls: ['./payment-card.component.css']
})
export class PaymentCardComponent {

  @Input() finishedIn: string;
  @Input() owner: string;
  @Input() expiration: string;
  @Input() id : string;

  constructor( 
    private toast: HotToastService,
    private dialog: MatDialog,
    private auth: AuthService,
    )
    {
    this.finishedIn = '';
    this.owner = '';
    this.expiration = '';
    this.id = '';
  }

  //open modals to edit and delete
  openEditModal(id : string, enterAnimations: string, exitAnimation: string){
    this.dialog.open(UpdateCardComponent, {
      data: {
        cardId: id,
        idCliente: this.auth.getClientId()
      },
      width: '90%',
      maxWidth: '400px',
      minHeight: '200px',
      maxHeight: '300px',
      enterAnimationDuration: enterAnimations,
      exitAnimationDuration: exitAnimation,
    })
  }
  openDeleteModal(enterAnimations: string, exitAnimation: string){
    this.dialog.open(DeleteCardComponent, {
      data: {
        CardId : this.id,
        IdCliente: this.auth.getClientId()
      },
      width: '90%',
      maxWidth: '400px',
      minHeight: '200px',
      maxHeight: '100px',
      enterAnimationDuration: enterAnimations,
      exitAnimationDuration: exitAnimation,
    })
  }

  //update form:
  form = new FormGroup({
     /* owner: new FormControl('', [Validators.required]), */
     expiration: new FormControl('', [Validators.required])
  })

  onSubmit(id: any){
  }
  //
}
