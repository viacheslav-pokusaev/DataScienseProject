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
import { untilDestroyed } from '@ngneat/until-destroy';
import { map } from 'rxjs/operators';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']  
})
export class GalleryComponent implements OnInit {


  onChange!: (value: string[]) => void;
  chipList!: MatChipList;
  value: string[] = [];
  checkList: string[] = [];



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

  public filterSelect(event: any, filterType: string) {
    this.filter.groupName = this.groupName;
    if (filterType === "tag") {
      this.filter.tagName = event.target.value;
    }
    else if (filterType === "executor") {
      this.filter.executorName = event.target.value;
    }

    this.homeService.getGalleryWithFilters(this.filter).subscribe((data: GalleryResult) => {
      this.galleryUnboxingData(data);
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








  ngAfterViewInit() {
    this.selectChips(this.value);
  }


  toggleSelection(chip: MatChip, index: number) {
    chip.toggleSelected();

    if (chip.selected) {
      this.checkList.push(chip.value);
      console.log(this.checkList);
    } else {
      console.log(this.checkList[index].toString());
      this.checkList.splice(index, 1);
    }
    

  }

















  selectChips(value: string[]) {
    this.chipList.chips.forEach((chip) => chip.deselect());

    const chipsToSelect = this.chipList.chips.filter((c) =>
      value.includes(c.value)
    );

    chipsToSelect.forEach((chip) => chip.select());
  }
  

}
