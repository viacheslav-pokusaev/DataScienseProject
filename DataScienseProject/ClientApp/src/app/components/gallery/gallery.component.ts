import { Component, OnInit } from '@angular/core';
import { GalleryModel } from '../../models/gallery/gallery.model';
import { HomeService } from '../../services/home.service';
import { GalleryResult } from '../../models/gallery-result.model';
import { StatusModel } from '../../models/status.model';
import { AuthorizeModel } from '../../models/authorize.model';
import { FilterModel } from '../../models/filter.model';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { MatChip } from '@angular/material/chips';
import { Title } from "@angular/platform-browser";

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {
  selectedTagsList: string[] = [];
  selectedExecutorsList: string[] = [];
  isModelExist: boolean;

  public tags: Set<string> = new Set;
  public executors: Set<string> = new Set;
  public filter: FilterModel = new FilterModel();

  public tagFilterValue: string;
  public executorFilterValue: string;

  public galleryModels: Array<GalleryModel>;
  public statusModel: StatusModel = new StatusModel();
  public groupName: string;
  constructor(private homeService: HomeService, private router: Router, private location: Location, private titleService: Title) {
    this.titleService.setTitle("Custom Solutions | Gallery");
  }

  ngOnInit() {
    sessionStorage.setItem('groupName', this.location.path().split("/", 2)[1]);
    this.groupName = sessionStorage.getItem('groupName');
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
      }
    });
  }

  private getGallery() {
    this.homeService.getGallery(this.groupName).subscribe(
      (data: GalleryResult) => {
        this.galleryUnboxingData(data);
      },
      error => {
        console.error('There was an error!', error);
      });
  }

  modelDetails(id: number, viewName: string) {
    this.homeService.setId(id);
    this.titleService.setTitle("Custom Solutions | Gallery | " + viewName);
    this.router.navigate([this.groupName, viewName.replace(/\s/g, '-').toLowerCase()]);
  }

  galleryUnboxingData(data: GalleryResult){
    if (data.statusModel.statusCode !== 403) {
      this.galleryModels = data.galleryModels;
      this.headerTextEcranizeLinks();
      data.galleryModels.forEach(gm => {
        this.tags = new Set(Array.from(this.tags).concat(Array.from(new Set(gm.tags))));
        this.executors = new Set(Array.from(this.executors).concat(Array.from(new Set(gm.executors))));
      });
    }
    this.statusModel = data.statusModel;
  }

  headerTextEcranizeLinks(){
    this.galleryModels.forEach((gm: GalleryModel)=> {
        for(let i = 0; i < gm.shortDescription.length; i++){
          if(gm.shortDescription[i]){
            let descriptionsArr = gm.shortDescription[i].split("<a");
            let newDescription = "";
            descriptionsArr.forEach(description => {
              let startIndex = description.indexOf("href");
              let endIndex = description.indexOf(">");
              description = description.replace(description.slice(startIndex, endIndex) + ">", "");
              description = description.replace("</a>", "");

              newDescription += description;
            });
            gm.shortDescription[i] = newDescription;
          };
        }
    })
  }

  tagsSelection(chip: MatChip, index: number) {
    chip.toggleSelected();
    if (chip.selected) {
      this.selectedTagsList.push(chip.value);
    } else {
      this.selectedTagsList.forEach((element, index) => {
        if (element == chip.value) this.selectedTagsList.splice(index, 1);
      });
    }
  }

  executorsSelection(chip: MatChip, index: number) {
    chip.toggleSelected();
    if (chip.selected) {
      this.selectedExecutorsList.push(chip.value);
    } else {
      this.selectedExecutorsList.forEach((element, index) => {
        if (element == chip.value) this.selectedExecutorsList.splice(index, 1);
      });
    }
  }

  selectTags() {
    this.filter.groupName = this.groupName;
    this.filter.tagsName = this.selectedTagsList;
    this.filter.executorsName = this.selectedExecutorsList;

    this.homeService.getGalleryWithFilters(this.filter).subscribe((data: GalleryResult) => {
      this.galleryUnboxingData(data);
      if (data.galleryModels.length !== 0) {
        this.isModelExist = true;
      } else {
        this.isModelExist = false;
      }
    });
  }

}
