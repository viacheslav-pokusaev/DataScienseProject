import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feedback } from '../models/feedback/feedback.model';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private http: HttpClient) { }

  

  public addFeedback(feedback: Feedback): Observable<any> {
    return this.http.post<any>('AddFeedback/add', feedback);
  }
}
