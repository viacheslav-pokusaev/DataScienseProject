import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public groupName: string;

  constructor(private location: Location, private router: Router) {
  }

  ngOnInit() {
    
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  getName() {
    this.groupName = sessionStorage.getItem('groupName');
    this.router.navigate([this.groupName]);
  }
}
