import { Component } from '@angular/core';
import { LayoutStyleModel } from '../../models/layout-style.model';
import { MainPageModel } from '../../models/main-page.model';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { HomeService } from '../../services/home.service';
import { ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent {

  public mainPageModel: MainPageModel;
  iframeHeight: string;
  isHeight: boolean = false;
  public iframeSrc: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer, private homeService: HomeService, private router: Router) {
  }

  ngOnInit() {
    var check = this.router.url;
    var splitted = check.split("/", 4);
    var currentId = Number(splitted[3]);
    this.homeService.currentId(currentId);
    this.homeService.getData().subscribe((data: MainPageModel) => {
      this.mainPageModel = data;
      data.layoutDataModels.find(val => {
        if (val.elementTypeName == "Iframe") {
          this.iframeSrc = this.sanitizeIframe(val.layoutStyleModel);
        }
     });
    },
      error => { console.error('There was an error!', error); });
  }

  sanitizeStyles(styles: Array<LayoutStyleModel>) {
    var styleRes: string = "";
    styles.forEach(style => {
      if (style.key !== "src") {           
        if (style.key !== "height" && this.isHeight) {
          styleRes += style.key + ": " + style.value + ";";          
          styleRes += this.iframeSize();          
        } else {
          styleRes += style.key + ": " + style.value + ";";          
        }
      }
    });
    return this.sanitizer.bypassSecurityTrustStyle(styleRes);
  }

  sanitizeIframe(styles: Array<LayoutStyleModel>) {
    var styleRes: string = "";
    styles.forEach(style => {
      if (style.key === "src") {
        styleRes += style.value;                
      }
    });
    return this.sanitizer.bypassSecurityTrustResourceUrl(styleRes);
  }

  sanitizeImage(styles: Array<LayoutStyleModel>, value: string) {
    var styleRes: string = "";
    styleRes += "<img src='" + value + "' style='" + this.sanitizeStyles(styles) + "'/>";
    return this.sanitizer.bypassSecurityTrustHtml(styleRes);
  }

  iframeSize() {
    this.isHeight = true;
    const iframeSize = document.getElementById('iframeSize');    
    return `height: ${iframeSize.style.height}`;
  }
}
