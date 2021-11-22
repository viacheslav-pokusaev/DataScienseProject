import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MainPageModel } from '../models/main-page.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public mainPageModel: MainPageModel;

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.http.get<MainPageModel>('GetData/main').subscribe(
      (data: MainPageModel) => {
        /*this.mainPageModel = data;*/
        console.log("Data: ", data);
      },
      error => {
        console.error('There was an error!', error);
      })
  }

}
