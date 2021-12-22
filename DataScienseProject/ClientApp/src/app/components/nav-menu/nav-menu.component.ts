import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public groupName: string;

  constructor(private location: Location) {    
  }

  ngOnInit() {
    this.groupName = this.getName();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  getName() {    
    return this.location.path();
  }
}
