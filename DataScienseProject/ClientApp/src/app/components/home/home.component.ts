import { Component } from '@angular/core';
import { LayoutStyleModel } from '../../models/layout-style.model';
import { MainPageModel } from '../../models/main-page.model';
import { DomSanitizer, SafeHtml, SafeResourceUrl } from '@angular/platform-browser';
import { HomeService } from '../../services/home.service';
import { ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { TrackingModel } from 'src/app/models/trackingModel.model';
import { LayoutDataModel } from 'src/app/models/layout-data.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent {

  public mainPageModel: MainPageModel;

  public clickDate: Date;

  public trackingModel: TrackingModel = new TrackingModel();

  private currentId: number;

  iframeHeight: string;
  isHeight: boolean = false;
  public iframeSrc: SafeResourceUrl;

  public headerImage: SafeHtml;

  isVisibleIframe: boolean;

  constructor(private sanitizer: DomSanitizer, private homeService: HomeService, private router: Router) {
  }

  ngOnInit() {
    this.currentId = +sessionStorage.getItem('viewId');
    this.configureTrackingModel();
    this.homeService.currentId(this.currentId);
    this.homeService.getData().subscribe((data: MainPageModel) => {
      this.mainPageModel = data;
      data.layoutDataModels.find(val => {
        this.configureData(val);
     });
    },
      error => { console.error('There was an error!', error); });
  }

  urlExists(url: string) {
    return fetch(url, { mode: "no-cors" })
      .then(res => this.isVisibleIframe = true)
      .catch(err => this.isVisibleIframe = false)
  }

  configureData(val: LayoutDataModel){
    switch(val.elementTypeName){
      case "Iframe":
        this.iframeSrc = this.sanitizeIframe(val.layoutStyleModel);        
        this.urlExists(val.layoutStyleModel.find(element => element.key == "src").value);
      break;
      case "Header Image":
        this.headerImage = this.sanitizeImage(val.layoutStyleModel, val.path, "Header Image");
      break;
    }
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

  sanitizeImage(styles: Array<LayoutStyleModel>, value: string, type: string) {
    var styleRes: string = "<img ";
    if(type === "Header Image") styleRes += "class='img-style img-style-ex img-border'";
    styleRes += " src='" + value + "' style='" + this.sanitizeStyles(styles) + "'/>";
    return this.sanitizer.bypassSecurityTrustHtml(styleRes);
  }

  iframeSize() {
    this.isHeight = true;
    const iframeSize = document.getElementById('iframeSize');
    return `height: ${iframeSize.style.height}`;
  }
  userClick(){
    this.trackingModel.visitLastClick = new Date();
  }

  configureTrackingModel(){
    this.homeService.getIPAddress().subscribe((res: any) => {
      this.trackingModel.viewKey = this.currentId;
      this.trackingModel.ipAddress = res.ip;
      this.trackingModel.isVisitSuccess = true;
      this.trackingModel.visitDate = new Date();
    });
  }

  ngOnDestroy(){
    this.homeService.sendTrackingData(this.trackingModel).subscribe((res: any)=>{});
  }

}
