import { Component, OnInit } from '@angular/core';
import { GalleryModel } from '../../models/gallery/gallery.model';
import { HomeService } from '../../services/home.service';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
import { GalleryResult } from 'src/app/models/gallery-result.model';
import { StatusModel } from 'src/app/models/status.model';
import { AuthorizeModel } from 'src/app/models/authorize.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  public galleryModels: Array<GalleryModel>;
  public statusModel: StatusModel = new StatusModel();
  public groupName: string;
  constructor( private homeService: HomeService, private router: Router) { }

  ngOnInit() {
    this.groupName = this.homeService.getGroupName();
    this.getGallery();
  }

  public login() {
    var authorizeData: AuthorizeModel = new AuthorizeModel();
    authorizeData.groupName = this.groupName;
    authorizeData.password = (<HTMLInputElement>document.getElementById("pass")).value;
    this.homeService.setAuthorize(authorizeData).subscribe((res: StatusModel) => {
      if (res.statusCode === 200) { this.getGallery(); }
      else { this.statusModel = res; alert(res.errorMessage); }
    });
  }

  private getGallery() {
    this.homeService.getGallery(this.groupName).subscribe((data: GalleryResult) => {
      if (data.exceptionModel.statusCode !== 403) { this.galleryModels = data.galleryModels; }
      this.statusModel = data.exceptionModel;
    },
      error => { console.error('There was an error!', error); });
  }

  modelDetails(id: number) {    
    this.homeService.getId(id);
    this.router.navigate(['gallery/model/', id]);
  }
}
