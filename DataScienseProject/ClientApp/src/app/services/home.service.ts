import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthorizeModel } from '../models/authorize.model';
import { StatusModel } from '../models/status.model';
import { GalleryResult } from '../models/gallery-result.model';
import { Feedback } from '../models/feedback/feedback.model';
import { MainPageModel } from '../models/main-page.model';
import { Router } from '@angular/router';
import { FilterModel } from '../models/filter.model';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  modelId: number;
  groupName: string;

  constructor(private http: HttpClient, private router: Router) {
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
  }

  addFeedback(feedback: Feedback) {
    feedback.viewKey = this.modelId;
    return this.http.post<any>('AddFeedback/add', feedback);
  }
  getGalleryWithFilters(filter: FilterModel){
    return this.http.post<GalleryResult>('GetData/gallery', filter);
  }

  getGroupName() {
    var str = this.router.url;
    var splitted = str.split("/", 2);    
    this.groupName = splitted[1];
    return this.groupName;
  } 
}
