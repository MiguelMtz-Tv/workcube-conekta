import { Component, HostListener, ElementRef, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.css']
})
export class DropdownComponent implements OnInit{
  isDropdownOpen = false;
  constructor(private elementRef: ElementRef, private router: Router){}

  toggle() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  @HostListener('document:click', ['$event']) onClick(event: MouseEvent) {
    if (!this.elementRef.nativeElement.contains(event.target)) {
      this.isDropdownOpen = false;
    }
  }

  ngOnInit() {
    this.router.events.subscribe((e) => {
      if(e instanceof NavigationEnd){
        this.isDropdownOpen = false;
      }
    })
  }
  
}
