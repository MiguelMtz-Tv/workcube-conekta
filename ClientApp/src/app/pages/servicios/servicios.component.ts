import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { HotToastService } from '@ngneat/hot-toast';


@Component({
  selector: 'app-servicios',
  templateUrl: './servicios.component.html',
  styleUrls: ['./servicios.component.css']
})
export class ServiciosComponent implements OnInit {
  constructor(private dataService: DataService, private toast: HotToastService){}

  services = [
    {
      id: '1',
      name: 'AWS',
      cost: "25000",
      desc: "Amazon Web Services es una colección de servicios de computación en la nube pública",
      status: "",
      startDate: "2023-04-12",
      period: "Mensual"
    },
    {
      id: '2',
      name: 'EOS',
      cost: "25000",
      desc: "The Eos is a Tau'ri spaceship commanded by the United States under the command of Colonel Freeman. It ",
      status: "",
      startDate: "2023-03-18",
      period: "Mensual"
    },
    {
      id: '3',
      name: 'Netflix',
      cost: "25000",
      desc: "Netflix, Inc. es una empresa de entretenimiento y una plataforma de streaming estadounidense.",
      status: "",
      startDate: "2023-03-23",
      period: "Mensual"
    }
  ]

  ngOnInit(): void {
    this.dataService.getCancelService().subscribe((id) => {
      let objIndex = this.services.findIndex((obj) => obj.id === id)
      this.services[objIndex].status='Cancelado'
      this.dataService.setModalClose('close')
    })
  }

}
