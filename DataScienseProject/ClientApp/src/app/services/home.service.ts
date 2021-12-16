import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GalleryModel } from '../models/gallery.model';
import { MainPageModel } from '../models/main-page.model';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  modelId: number;

  constructor(private http: HttpClient) {       
  }

  getData() {
    return this.http.get<MainPageModel>('GetData/gallery/model/' + this.modelId);
  };

  getGallery(groupName: string) {
    return this.http.get<Array<GalleryModel>>('GetData/gallery/' + groupName);
  }

  getId(id) {    
    this.modelId = id;
  }
}
