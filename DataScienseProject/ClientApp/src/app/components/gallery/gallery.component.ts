import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { GalleryModel } from '../../models/gallery.model';
import { HomeService } from '../../services/home.service';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
import { GalleryResult } from 'src/app/models/gallery-result.model';
import { ExceptionModel } from 'src/app/models/exception.model';
import { AuthorizeModel } from 'src/app/models/authorize.model';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  public galleryModels: Array<GalleryModel>;
  public exceptionModel: ExceptionModel = new ExceptionModel();
  public groupName: string = "Group1";
  constructor(private homeService: HomeService, config: NgbCarouselConfig) {
    config.interval = 5000;
  }

  ngOnInit() {
    this.getGallery();
  }

public login() {
  var authorizeData: AuthorizeModel = new AuthorizeModel();
  authorizeData.groupName = this.groupName;
  authorizeData.password = (<HTMLInputElement>document.getElementById("pass")).value;
  this.homeService.setAuthorize(authorizeData).subscribe((res: ExceptionModel) => {
      if(res.statusCode === 200){
        this.getGallery();
      }
      else{
        this.exceptionModel = res;
        alert(res.errorMessage);
      }
    });
  }

  private getGallery(){
    this.homeService.getGallery(this.groupName).subscribe(
      (data: GalleryResult) => {
        if(data.exceptionModel.statusCode !== 403){
          this.galleryModels = data.galleryModels;
        }
          this.exceptionModel = data.exceptionModel;
      },
      error => {
        console.error('There was an error!', error);
      });
  }
}

