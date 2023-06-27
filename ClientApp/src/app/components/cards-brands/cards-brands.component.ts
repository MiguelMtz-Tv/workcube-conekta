import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-cards-brands',
  templateUrl: './cards-brands.component.html',
  styleUrls: ['./cards-brands.component.css']
})
export class CardsBrandsComponent {
  @Input() brand!: string
}
