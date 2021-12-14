import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { ExecutorModel } from '../../models/executor.model';
import { LayoutStyleModel } from '../../models/layout-style.model';
import { LayoutDataModel } from '../../models/layout-data.model';
import { MainPageModel } from '../../models/main-page.model';
import { ProjectTypeModel } from '../../models/project-type.model';
import { TechnologyModel } from '../../models/technology.model';
import { DomSanitizer } from '@angular/platform-browser';
import { HomeService } from '../../services/home.service';
import { ViewEncapsulation } from '@angular/core'


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  encapsulation: ViewEncapsulation.None
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

  sanitizeStyles(styles: Array<LayoutStyleModel>) {

    var styleRes: string = "";
    styles.forEach(style =>{
      if(style.key !== "src"){
        var styleString = style.key + ": " + style.value + ";";
        styleRes += styleString;              
      }      
    });
    return this.sanitizer.bypassSecurityTrustStyle(styleRes);
  }

  sanitizeIframe(styles: Array<LayoutStyleModel>) {
    var styleRes: string = "";
    styles.forEach(style => {
      if (style.key === "src") {
        var styleString = style.value;
        styleRes += styleString;
      }
    });
    return this.sanitizer.bypassSecurityTrustResourceUrl(styleRes);
  }

  sanitizeImage(styles: Array<LayoutStyleModel>) {   
    var styleRes: string = "";
    var base64: string = "";
    styles.forEach(style => {
      if (style.key == "src") {
        base64 += style.value;
      }     
    });
    var styleString = "<img src='" + base64 + "' style='" + this.sanitizeStyles(styles) + "'/>";
    styleRes += styleString;
    return this.sanitizer.bypassSecurityTrustHtml(styleRes);
  }

}
