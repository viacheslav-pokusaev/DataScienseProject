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
  selectedTagsList: string[] = [];
  selectedExecutorsList: string[] = [];


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

  tagsSelection(chip: MatChip, index: number) {
    chip.toggleSelected();
    if (chip.selected) {
      this.selectedTagsList.push(chip.value);
      console.log(this.selectedTagsList);
    } else {   
      this.selectedTagsList.forEach((element, index) => {
        if (element == chip.value) this.selectedTagsList.splice(index, 1);
      });      
      console.log(this.selectedTagsList);
    }   
  }

  executorsSelection(chip: MatChip, index: number) {
    chip.toggleSelected();
    if (chip.selected) {
      this.selectedExecutorsList.push(chip.value);
      console.log(this.selectedExecutorsList);
    } else {
      this.selectedExecutorsList.forEach((element, index) => {
        if (element == chip.value) this.selectedExecutorsList.splice(index, 1);
      });
      console.log(this.selectedExecutorsList);
    }
  }

  checkButton() {
    console.log(this.selectedTagsList);

    this.filter.groupName = this.groupName;
    this.filter.tagsName = this.selectedTagsList;
    this.filter.executorsName = this.selectedExecutorsList;

    this.homeService.getGalleryWithFilters(this.filter).subscribe((data: GalleryResult) => {
      this.galleryUnboxingData(data);
    });

  }

}
