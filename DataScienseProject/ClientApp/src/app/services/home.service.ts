import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthorizeModel } from '../models/authorize.model';
import { StatusModel } from '../models/status.model';
import { GalleryResult } from '../models/gallery-result.model';
import { Feedback } from '../models/feedback/feedback.model';
import { GalleryModel } from '../models/gallery/gallery.model';
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
    return this.http.get<GalleryResult>('GetData/gallery/' + groupName);
  }

  setAuthorize(authorizeModel: AuthorizeModel){
    return this.http.post<StatusModel>('Authorize/checkPass', authorizeModel);
  }

  getId(id) {    
    this.modelId = id;
  }

  addFeedback(feedback: Feedback) {
    feedback.viewKey = this.modelId;
    return this.http.post<any>('AddFeedback/add', feedback);
  }
}
