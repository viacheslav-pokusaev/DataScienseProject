import { Component } from '@angular/core';
import { LayoutStyleModel } from '../../models/layout-style.model';
import { MainPageModel } from '../../models/main-page.model';
import { DomSanitizer } from '@angular/platform-browser';
import { HomeService } from '../../services/home.service';
import { ViewEncapsulation } from '@angular/core';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent {

  public mainPageModel: MainPageModel;

  public clickDate: Date;

  iframeHeight: string;
  constructor(private sanitizer: DomSanitizer, private homeService: HomeService) {
  }

  ngOnInit() {

    this.homeService.getData().subscribe((data: MainPageModel) => {
      this.mainPageModel = data;
    },
      error => { console.error('There was an error!', error); });
  }

  sanitizeStyles(styles: Array<LayoutStyleModel>) {
    var styleRes: string = "";
    styles.forEach(style => {
      if (style.key !== "src") {
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
    styles.forEach(style => {
      if (style.key === "height") {
        this.iframeHeight = style.key + ": " + style.value + ";";
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

  userClick(){
    this.clickDate = new Date();
  }

}
