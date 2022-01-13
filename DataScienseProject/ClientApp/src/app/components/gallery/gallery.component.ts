import { Component, OnInit } from '@angular/core';
import { GalleryModel } from '../../models/gallery/gallery.model';
import { HomeService } from '../../services/home.service';
import { GalleryResult } from '../../models/gallery-result.model';
import { StatusModel } from '../../models/status.model';
import { AuthorizeModel } from '../../models/authorize.model';
import { FilterModel } from '../../models/filter.model';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { MatChip, MatChipList, MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']  
})
export class GalleryComponent implements OnInit {
  selectedTagList: string[] = [];

  public tags: Set<string> = new Set;
  public executors: Set<string> = new Set;
  public filter: FilterModel = new FilterModel();

  public tagFilterValue: string;
  public executorFilterValue: string;

  public galleryModels: Array<GalleryModel>;
  public statusModel: StatusModel = new StatusModel();
  public groupName: string;
  constructor(private homeService: HomeService, private router: Router, private location: Location) { }

  ngOnInit() {
    sessionStorage.setItem('groupName', this.location.path());
    this.groupName = this.homeService.getGroupName();
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

  //public filterSelect(event: any, filterType: string) {
  //  this.filter.groupName = this.groupName;
  //  if (filterType === "tag") {
  //    this.filter.tagName = event.target.value;
  //  }
  //  else if (filterType === "executor") {
  //    this.filter.executorName = event.target.value;
  //  }

  //  this.homeService.getGalleryWithFilters(this.filter).subscribe((data: GalleryResult) => {
  //    this.galleryUnboxingData(data);
  //  });
  //}

  modelDetails(id: number) {
    this.homeService.setId(id);
    this.router.navigate(['gallery/model/', id]);
  }

  galleryUnboxingData(data: GalleryResult){
    if (data.statusModel.statusCode !== 403) {
      this.galleryModels = data.galleryModels;
      data.galleryModels.forEach(gm => {
        this.tags = new Set(Array.from(this.tags).concat(Array.from(new Set(gm.tags))));
        this.executors = new Set(Array.from(this.executors).concat(Array.from(new Set(gm.executors))));
      });
    }
    this.statusModel = data.statusModel;
  }

  toggleSelection(chip: MatChip, index: number) {
    chip.toggleSelected();
    if (chip.selected) {
      this.selectedTagList.push(chip.value);
      console.log(this.selectedTagList);
    } else {   
      this.selectedTagList.forEach((element, index) => {
        if (element == chip.value) this.selectedTagList.splice(index, 1);
      });      
      console.log(this.selectedTagList);
    }   
  }

  checkButton() {
    console.log(this.selectedTagList);

    this.filter.groupName = this.groupName;

    this.filter.tagName = this.selectedTagList;

    this.homeService.getGalleryWithFilters(this.filter).subscribe((data: GalleryResult) => {
      this.galleryUnboxingData(data);
    });

  }



}
