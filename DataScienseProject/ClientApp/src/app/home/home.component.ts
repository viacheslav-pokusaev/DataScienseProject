import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { LayoutStyleModel } from '../models/layout-style.model';
import { MainPageModel } from '../models/main-page.model';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{

  public mainPageModel: MainPageModel;
  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {
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
