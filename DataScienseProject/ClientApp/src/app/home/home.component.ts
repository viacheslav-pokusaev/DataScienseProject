import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {


  constructor(private http: HttpClient) {
  }
  
  check: any;

  ngOnInit() {
    this.http.get<any>('GetData/main').subscribe({
      next: data => {
        this.check = data;
      },
      error: error => {        
        console.error('There was an error!', error);
      }
    })
  }

}
