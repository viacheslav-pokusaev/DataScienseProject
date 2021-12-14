import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GalleryResult } from '../models/gallery-result.model';
import { MainPageModel } from '../models/main-page.model';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) {
  }

  getData() {
    return this.http.get<MainPageModel>('GetData/main');
  };

  getGallery(groupName: string) {
    return this.http.get<GalleryResult>('GetData/gallery/' + groupName);
  }

}
