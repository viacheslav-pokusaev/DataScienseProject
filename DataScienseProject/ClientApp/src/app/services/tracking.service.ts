import { HttpClient } from "@angular/common/http";

export class TrackingService{

  constructor(private http: HttpClient){}

  sendData(){
    this.http.post('', Object);
  }
}
