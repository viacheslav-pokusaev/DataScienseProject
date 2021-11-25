import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { ExecutorModel } from '../models/executor.model';
import { LayoutStyleModel } from '../models/layout-style.model';
import { LayoutDataModel } from '../models/layout-data.model';
import { MainPageModel } from '../models/main-page.model';
import { ProjectTypeModel } from '../models/project-type.model';
import { TehnologyModel } from '../models/tehnology.model';
import { DomSanitizer } from '@angular/platform-browser';
import { HomeService } from '../services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{

  public mainPageModel: MainPageModel;
  constructor(private http: HttpClient, private sanitizer: DomSanitizer, private homeService: HomeService) {
  }

  ngOnInit() {

    this.homeService.getData().subscribe(    
      (data: MainPageModel) => {
        this.mainPageModel = data;
      },
      error => {
        console.error('There was an error!', error);
      })
  }

  sanitize(styles: Array<LayoutStyleModel>) {

    var styleRes: string = "";
    styles.forEach(style =>{
      if(style.key !== "src"){
        var styleString = style.key + ": " + style.value + ";";
        styleRes += styleString;
      }
    });
    return this.sanitizer.bypassSecurityTrustStyle(styleRes);
  }

}
