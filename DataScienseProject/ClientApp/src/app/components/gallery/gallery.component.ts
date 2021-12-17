import { Component, OnInit } from '@angular/core';
import { GalleryModel } from '../../models/gallery/gallery.model';
import { HomeService } from '../../services/home.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  public galleryModels: Array<GalleryModel>;
  public groupName: string = "Group1";
  constructor( private homeService: HomeService, private router: Router) {
    
  }

  ngOnInit() {

    this.homeService.getGallery(this.groupName).subscribe(
      (data: Array<GalleryModel>) => {
        this.galleryModels = data;
      },
      error => {
        console.error('There was an error!', error);
      })
  }

  modelDetails(id: number) {    
    this.homeService.getId(id);
    this.router.navigate(['gallery/model/', id]);
  }
}
