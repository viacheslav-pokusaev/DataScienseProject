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

  public executorModel: Array<ExecutorModel>;
  public layoutStyleModel: Array<LayoutStyleModel>;
  public layoutDataModel: Array<LayoutDataModel>;
  public projectTypeModel: Array<ProjectTypeModel>;
  public tehnologyModel: Array<TehnologyModel>;

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.http.get<MainPageModel>('GetData/main').subscribe(
      (data: MainPageModel) => {
        this.mainPageModel = data;

        this.projectTypeModel = this.mainPageModel.projectTypeModels;
        this.tehnologyModel = this.mainPageModel.tehnologyModels;
        this.layoutDataModel = this.mainPageModel.layoutDataModels;
        this.layoutStyleModel = this.mainPageModel.layoutStyleModels;

      },
      error => {
        console.error('There was an error!', error);
      })
  }

}
