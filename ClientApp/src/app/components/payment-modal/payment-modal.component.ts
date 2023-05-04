import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-payment-modal',
  templateUrl: './payment-modal.component.html',
  styleUrls: ['./payment-modal.component.css']
})
export class PaymentModalComponent {
  isOpen = false;
  @Input() service = ''

  constructor(){

  }
  toggle() {
    this.isOpen = !this.isOpen;
  }
}
