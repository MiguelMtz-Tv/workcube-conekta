import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ChangeDetectionStrategy } from '@angular/core';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-navbar-menu',
  templateUrl: './navbar-menu.component.html',
  styleUrls: ['./navbar-menu.component.css']
})
export class NavbarMenuComponent {

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
  ngOnInit(){
    this.router.events.subscribe((e)=>{
      if(e instanceof NavigationEnd){
        this.activeLink = e.url
      }
    })
  }

  closeSession(){
    this.auth.removeData();
    this.router.navigate(['/'])
  }
}
