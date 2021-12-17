import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthorizeModel } from '../models/authorize.model';
import { ExceptionModel } from '../models/status.model';
import { GalleryResult } from '../models/gallery-result.model';
import { MainPageModel } from '../models/main-page.model';
import { FilterModel } from '../models/filter.model';

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

  setAuthorize(authorizeModel: AuthorizeModel){
    return this.http.post<ExceptionModel>('Authorize/checkPass', authorizeModel);
  }

  getGalleryWithFilters(groupName: string, filter: FilterModel){
    return this.http.post<GalleryResult>('GetData/gallery/' + groupName, filter);
  }
}
