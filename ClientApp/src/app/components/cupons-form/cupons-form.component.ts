import { Component } from '@angular/core';

@Component({
  selector: 'app-cupons-form',
  templateUrl: './cupons-form.component.html',
  styleUrls: ['./cupons-form.component.css']
})
export class CuponsFormComponent {
  isOpen = false;
  
  toggle(){
    this.isOpen = !this.isOpen
  }
}
