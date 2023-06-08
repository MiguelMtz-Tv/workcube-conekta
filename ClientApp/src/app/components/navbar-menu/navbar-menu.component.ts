import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ChangeDetectionStrategy } from '@angular/core';
import {MatDialog, MatDialogRef, MatDialogModule} from '@angular/material/dialog';
import { CuponsDialogComponent } from '../dialogs/cupons-dialog/cupons-dialog.component';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-navbar-menu',
  templateUrl: './navbar-menu.component.html',
  styleUrls: ['./navbar-menu.component.css']
})
export class NavbarMenuComponent {

  public activeLink: string = '/servicios'

  constructor(private router: Router, private auth: AuthService, private dialog: MatDialog){}

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

  showCupons(enterAnimations: string, exitAnimation: string): void{
    this.dialog.open(CuponsDialogComponent, {
        width: '90%',
        maxWidth: '500px',
        minHeight: '300px',
        maxHeight: '1000px',
        enterAnimationDuration: enterAnimations,
        exitAnimationDuration: exitAnimation,
    })
  }
}
