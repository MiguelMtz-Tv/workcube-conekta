import { Component, OnInit, Input } from '@angular/core';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  isOpen = false

  constructor(private dataService: DataService) { }

  toggle() {
    this.isOpen = !this.isOpen;
  }

  ngOnInit(): void {
    this.dataService.getModalClose().subscribe(() => {
      this.isOpen = false
    })
  }
}
