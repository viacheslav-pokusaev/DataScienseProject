import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TagModel } from '../models/main-page/tag.model';

@Injectable({
  providedIn: 'root'
})
export class MainPageService {

  constructor(private http: HttpClient) { }

  getTags() {
    return this.http.get<Array<TagModel>>('MainPage/tags');
  };

  sendTags(tags: Array<TagModel>) {
    return this.http.post('MainPage/add', tags);
  }
}
