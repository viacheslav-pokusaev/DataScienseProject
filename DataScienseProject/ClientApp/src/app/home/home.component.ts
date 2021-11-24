import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ExecutorModel } from '../models/executor.model';
import { LayoutStyleModel } from '../models/layout-style.model';
import { LayoutDataModel } from '../models/layout-data.model';
import { MainPageModel } from '../models/main-page.model';
import { ProjectTypeModel } from '../models/project-type.model';
import { TehnologyModel } from '../models/tehnology.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',  
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  public mainPageModel: MainPageModel;

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.http.get<MainPageModel>('GetData/main').subscribe(
      (data: MainPageModel) => {
        this.mainPageModel = data;
      },
      error => {
        console.error('There was an error!', error);
      })
  }

}
