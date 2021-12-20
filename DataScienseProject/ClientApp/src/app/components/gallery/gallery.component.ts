import { Component, OnInit } from '@angular/core';
import { GalleryModel } from '../../models/gallery.model';
import { HomeService } from '../../services/home.service';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
import { GalleryResult } from 'src/app/models/gallery-result.model';
import { ExceptionModel } from 'src/app/models/status.model';
import { AuthorizeModel } from 'src/app/models/authorize.model';
import { ExecutorModel } from 'src/app/models/executor.model';
import { FilterModel } from 'src/app/models/filter.model';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  public tags: Array<string> = new Array;
  public executors: Array<ExecutorModel> = new Array;

  public tagFilterValue: string;
  public executorFilterValue: string;

  public galleryModels: Array<GalleryModel> = new Array;
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
      if (res.statusCode === 200) {
        this.getGallery();
      }
      else {
        this.exceptionModel = res;
        alert(res.errorMessage);
      }
    });
  }

  private getGallery() {
    this.homeService.getGallery(this.groupName).subscribe(
      (data: GalleryResult) => {
        if (data.exceptionModel.statusCode !== 403) {
          data.galleryModels.forEach(gm => {
            if (this.galleryModels.find(e => e.viewKey == gm.viewKey) === undefined) {
              this.galleryModels.push(gm);
            }
          });
          data.galleryModels.forEach(gm => {
            gm.tags.forEach(t => {
              if (this.tags.indexOf(t) === -1) {
                this.tags.push(t);
              }
            });
            gm.executors.forEach(ex => {
              if (this.executors.find(e => e.executorName == ex.executorName) === undefined) {
                this.executors.push(ex);
              }
            });
          });
        }
        this.exceptionModel = data.exceptionModel;
      },
      error => {
        console.error('There was an error!', error);
      });
  }

  public filterSelect(event: any, filterType: string) {
    let filter = new FilterModel();
    filter.groupName = this.groupName;
    if (filterType === "tag") {
      this.tagFilterValue = event.target.value;
    }
    else if (filterType === "executor") {
      this.executorFilterValue = event.target.value;
    }

    filter.tagName = this.tagFilterValue;
    filter.executorName = this.executorFilterValue;

    this.homeService.getGalleryWithFilters(filter).subscribe((data: GalleryResult) => {
      if (data.exceptionModel.statusCode !== 403) {
        this.galleryModels = new Array;
        data.galleryModels.forEach(gm => {
          if (this.galleryModels.find(e => e.viewKey === gm.viewKey) === undefined) {
            this.galleryModels.push(gm);
          }
        });
        data.galleryModels.forEach(gm => {
          gm.tags.forEach(t => {
            if (this.tags.indexOf(t) === -1) {
              this.tags.push(t);
            }
          });
          gm.executors.forEach(ex => {
            if (this.executors.find(e => e.executorName == ex.executorName) === undefined) {
              this.executors.push(ex);
            }
          });
        });
      }
      this.exceptionModel = data.exceptionModel;
    });
  }
}

