import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-cupon',
  templateUrl: './cupon.component.html',
  styleUrls: ['./cupon.component.css']
})
export class CuponComponent {
@Input() monto: any
@Input() codigo: any
@Input() descripcion: any
@Input() vigencia: any
}
