import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar-menu',
  templateUrl: './navbar-menu.component.html',
  styleUrls: ['./navbar-menu.component.css']
})
export class NavbarMenuComponent implements OnInit {
  public href: string = ''
  public activeLink: string = '/servicios'

  constructor(private router: Router, private auth: AuthService){}

  links = [
    {
      name: 'Mis servicios',
      path: '/servicios'
    },
    {
      name: 'Tarjetas',
      path: '/tarjetas'
    }
  ];
  ngOnInit(): void {
    this.router.events.subscribe((e)=>{
      if(e instanceof NavigationEnd){
        this.activeLink = e.url
      }
    })
  }

  closeSession(){
    this.auth.removeToken();
    this.router.navigate(['/'])
  }
}
