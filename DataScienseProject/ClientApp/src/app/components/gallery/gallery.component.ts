import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { GalleryModel } from '../../models/gallery.model';
import { HomeService } from '../../services/home.service';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  public galleryModel: Array<GalleryModel>;
  public groupName: string;
  constructor(private http: HttpClient, private sanitizer: DomSanitizer, private homeService: HomeService) {
  }

  ngOnInit() {

    this.homeService.getGallery(this.groupName).subscribe(
      (data: Array<GalleryModel>) => {
        this.galleryModel = data;
      },
      error => {
        console.error('There was an error!', error);
      })
  }

}
