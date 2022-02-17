import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataToSendModel } from '../models/main-page/data-to-send';
import { TagModel } from '../models/main-page/tag.model';

@Injectable({
  providedIn: 'root'
})
export class MainPageService {

  constructor(private http: HttpClient) { }

  getTags() {
    return this.http.get<Array<TagModel>>('MainPage/tags');
  };

  sendTags(dataToSendModel: DataToSendModel ) {
    return this.http.post('MainPage/add', dataToSendModel);
  }
}
