import { Component, OnInit } from '@angular/core';
import { GalleryModel } from '../../models/gallery/gallery.model';
import { HomeService } from '../../services/home.service';
import { GalleryResult } from '../../models/gallery-result.model';
import { StatusModel } from '../../models/status.model';
import { ExecutorModel } from '../../models/executor.model';
import { AuthorizeModel } from '../../models/authorize.model';
import { FilterModel } from '../../models/filter.model';
import { Router } from '@angular/router';


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

  public galleryModels: Array<GalleryModel>;
  public statusModel: StatusModel = new StatusModel();
  public groupName: string = "Group1";

  constructor(private homeService: HomeService, private router: Router) { }

  ngOnInit() {
    this.getGallery();
  }

  public login() {
    var authorizeData: AuthorizeModel = new AuthorizeModel();
    authorizeData.groupName = this.groupName;
    authorizeData.password = (<HTMLInputElement>document.getElementById("pass")).value;
    this.homeService.setAuthorize(authorizeData).subscribe((res: StatusModel) => {
      if (res.statusCode === 200) {
        this.getGallery();
      }
      else {
        this.statusModel = res;
        alert(res.errorMessage);
      }
    });
  }

  private getGallery() {
    this.homeService.getGallery(this.groupName).subscribe(
      (data: GalleryResult) => {
        if (data.statusModel.statusCode !== 403) {
          this.galleryModels = data.galleryModels;
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
        this.statusModel = data.statusModel;
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
      if (data.statusModel.statusCode !== 403) {
        this.galleryModels = data.galleryModels;
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
      this.statusModel = data.statusModel;
    });
  }
  modelDetails(id: number) {
    this.homeService.getId(id);
    this.router.navigate(['gallery/model/', id]);
  }
}
