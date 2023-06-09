import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit{
  title = 'app';
  layout = true;
  login = false;
  path = '';
  
  constructor(private location: Location, private router: Router){
  }

  ngOnInit() {
    this.router.events.subscribe((e) => {
      if (e instanceof NavigationEnd) {
        this.path = this.location.path();
        if (this.path === '' || this.path === '/registro' || this.path === '/expired') {
          this.login = true;
          this.layout = false;
        } 
        // condicion con fines de testeo
        else if(this.path === '/cliente'){
          this.layout = false;
          this.login = true
        }
        // condicion con fines de testeo
        else{
          this.layout = true;
          this.login = false;
        }
      }
    })
  }
}
