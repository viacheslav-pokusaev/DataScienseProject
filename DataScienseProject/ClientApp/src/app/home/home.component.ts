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

        //this.executorModel = this.mainPageModel.executorModels;
        //this.layoutStyleModel = this.mainPageModel.layoutStyleModels;
        //this.layoutDataModel = this.mainPageModel.layoutDataModels;
        //this.projectTypeModel = this.mainPageModel.projectTypeModels;
        //this.tehnologyModel = this.mainPageModel.tehnologyModels;

        this.projectTypeModel = this.mainPageModel.projectTypeModels;
        this.layoutDataModel = this.mainPageModel.layoutDataModels.filter(e => e.elementTypeName == "Html paragraph");



             

        
      },
      error => {
        console.error('There was an error!', error);
      })
  }

}
