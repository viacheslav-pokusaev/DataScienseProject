import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthorizeModel } from '../models/authorize.model';
import { StatusModel } from '../models/status.model';
import { GalleryResult } from '../models/gallery-result.model';
import { Feedback } from '../models/feedback/feedback.model';
import { MainPageModel } from '../models/main-page.model';
import { Router } from '@angular/router';
import { FilterModel } from '../models/filter.model';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  modelId: number;
  groupName: string;

  constructor(private http: HttpClient, private router: Router, private location: Location) {
  }

  currentId(currId: number) {
    this.modelId = currId;
  }

  getData() {
    return this.http.get<MainPageModel>('GetData/gallery/model/' + this.modelId);
  };

  getGallery(groupName: string) {
    return this.http.get<GalleryResult>('GetData/gallery/' + groupName);
  }

  setAuthorize(authorizeModel: AuthorizeModel){
    return this.http.post<StatusModel>('Authorize/checkPass', authorizeModel);
  }

  setId(id) {
    this.modelId = id;
    sessionStorage.setItem('viewId', this.modelId.toString());
  } 

  addFeedback(feedback: Feedback) {
    feedback.viewKey = this.modelId;
    return this.http.post<any>('AddFeedback/add', feedback);
  }
  getGalleryWithFilters(filter: FilterModel){
    return this.http.post<GalleryResult>('GetData/gallery', filter);
  }  
}
