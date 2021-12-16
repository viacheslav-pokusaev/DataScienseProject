import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { GalleryModel } from '../../models/gallery.model';
import { HomeService } from '../../services/home.service';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  public galleryModels: Array<GalleryModel>;
  public groupName: string = "Group1";
  constructor(private http: HttpClient, private sanitizer: DomSanitizer, private homeService: HomeService, config: NgbCarouselConfig, private router: Router) {
    config.interval = 5000;
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
