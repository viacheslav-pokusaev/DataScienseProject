import { Component, OnInit } from '@angular/core';
import { ViewEncapsulation } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Feedback } from '../../models/feedback.model';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class FooterComponent implements OnInit {

  firstFormGroup: FormGroup;
  feedback: Feedback = new Feedback();
  name: string = "";
  email: string = "";
  message: string = "";

  constructor(private _formBuilder: FormBuilder) {

  }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      nameCtrl: ['', [Validators.required]],
      emailCtrl: ['', [Validators.required, Validators.email]],
      aboutCtrl: ['', [Validators.required, Validators.maxLength(500)]]
    });

  }

  sendFeedback() {    
    this.feedback.name = this.firstFormGroup.value.nameCtrl;
    this.feedback.email = this.firstFormGroup.value.emailCtrl;
    this.feedback.about = this.firstFormGroup.value.aboutCtrl;
    this.name = "";
    this.email = "";
    this.message = "";
  }

}
