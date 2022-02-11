import { transition, trigger, useAnimation } from '@angular/animations';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { Email } from '../../../assets/smtp.js';
/*import { fadeInLeft, fadeInRight } from 'ng-animate';*/

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  @Input() theme: string;
  fadeInLeft: any;
  fadeInRight: any;

  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;

  toppings = new FormControl('', Validators.required);
  toppingList: string[] = ['resource accounting (money, time, personnel, tasks)', 'bookkeeping', 'production', 'logistics', 'interaction with suppliers', 'other option'];

  auditText: string = "Audit";
  nameFirstStep: string = "Fill out your data";
  nameSecondStep: string = "Company in Common";
  nameThirdStep: string = "Weaknesses";
  textButton: string = "Next";
  isLinear = true;
  turnAnimation = false;

  constructor(private modalService: NgbModal, private _formBuilder: FormBuilder) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      nameCtrl: ['', [Validators.required]],
      companyCtrl: ['', [Validators.required]],
      phoneCtrl: ['', [Validators.required, Validators.pattern("(([+][(]?[0-9]{1,3}[)]?)|([(]?[0-9]{4}[)]?))\s*[)]?[-\s\.]?[(]?[0-9]{1,3}[)]?([-\s\.]?[0-9]{3})([-\s\.]?[0-9]{3,4})")]],
      emailCtrl: ['', [Validators.required, Validators.email]],
      skypeCtrl: '',
      aboutCtrl: ['', [Validators.maxLength(500)]],
      checked: false
    });
    this.secondFormGroup = this._formBuilder.group({
      nameIndustryCtrl: ['', [Validators.required]],
      unprofitableProcessCtrl: '',
      resourcesMaximumCtrl: '',
      clearWeaknessesCtrl: '',
      whyOptimizeCtrl: ['', [Validators.maxLength(700)]]
    });
    this.thirdFormGroup = this._formBuilder.group({
      operationalCapabilitiesCtrl: ['', [Validators.maxLength(500)]],
      indicatorsSatisfyCtrl: ['', [Validators.maxLength(500)]],
      suitsCompletelyCtrl: ['', [Validators.maxLength(500)]],
      idealBusinessCtrl: ['', [Validators.maxLength(2000)]],
      ideaImproveCtrl: ''
    });
  }

  openVerticallyCentered(content) {
    this.modalService.open(content, { centered: true, windowClass: "MoveOn", backdrop: 'static' });
    document.body.style.paddingRight = "0px";

    setTimeout(() => {
      let headerSteps = document.getElementsByClassName('mat-step-header');
      for (let index = 0; index < headerSteps.length; index++) {
        let element = headerSteps[index] as HTMLElement;
        element.style.pointerEvents = "none";
      }
    }, 555);
  }

  changeTextButton() {
    if (this.firstFormGroup.controls["checked"].value == true)
      this.textButton = "Send Request";
    else this.textButton = "Next step";
  }

  nextStage(stepper: MatStepper, sendRequest?: boolean) {

    if (this.firstFormGroup.controls["checked"].value == true) {
      this.isLinear = false;
      this.goToLast(stepper);
    } else {
      this.checkStage(stepper.selectedIndex);
      if (sendRequest == true) {
        this.onSubmit(true);
        this.auditText = "Congratulations!";
        setTimeout(() => {
          this.turnAnimation = true;
        }, 10);
      }
      stepper.next();
    }
  }

  fixLastStepIcon() {
    let editElement = document.getElementsByClassName('mat-step-icon-state-edit')[0] as HTMLElement;
    let doneElement = document.getElementsByClassName('mat-step-icon-state-done')[0] as HTMLElement;
    let doneElementPaste = document.getElementsByClassName('mat-step-icon-state-number')[1] as HTMLElement;

    let clone;
    if (editElement !== undefined) {
      clone = editElement.cloneNode(true);
    } else if (doneElement !== undefined) {
      clone = doneElement.cloneNode(true);
    }

    doneElementPaste.replaceWith(clone);
  }

  checkStage(numberStep: number) {
    switch (numberStep) {
      case 0:
        if (this.firstFormGroup.valid)
          this.nameFirstStep = 'Welcome, ' + this.firstFormGroup.controls["nameCtrl"].value;
        break;
      case 1:
        this.nameSecondStep = "Complete";
        break;
      case 2:
        this.nameThirdStep = "Complete";
        break;
    }
  }

  goToLast(stepper: MatStepper) {
    this.nameFirstStep = 'Welcome, ' + this.firstFormGroup.controls["nameCtrl"].value;
    this.clearValidators();
    setTimeout(() => {
      this.turnAnimation = true;
    }, 2000);
    setTimeout(() => {
      stepper.next();
      setTimeout(() => {
        this.nameSecondStep = "Skipping";
        stepper.next();
        setTimeout(() => {
          this.nameThirdStep = "Skipping";
          stepper.next();
          this.auditText = "Congratulations!";
          this.onSubmit(false);
        }, 800);
      }, 800);
    }, 800);
  }

  clearValidators() {
    this.secondFormGroup.controls["nameIndustryCtrl"].clearValidators();
    this.secondFormGroup.controls["nameIndustryCtrl"].updateValueAndValidity();
    this.secondFormGroup.controls["whyOptimizeCtrl"].clearValidators();
    this.secondFormGroup.controls["whyOptimizeCtrl"].updateValueAndValidity();
    this.toppings.clearValidators();
    this.toppings.updateValueAndValidity();

    this.thirdFormGroup.controls["operationalCapabilitiesCtrl"].clearValidators();
    this.thirdFormGroup.controls["operationalCapabilitiesCtrl"].updateValueAndValidity();
    this.thirdFormGroup.controls["indicatorsSatisfyCtrl"].clearValidators();
    this.thirdFormGroup.controls["indicatorsSatisfyCtrl"].updateValueAndValidity();
    this.thirdFormGroup.controls["suitsCompletelyCtrl"].clearValidators();
    this.thirdFormGroup.controls["suitsCompletelyCtrl"].updateValueAndValidity();
    this.thirdFormGroup.controls["idealBusinessCtrl"].clearValidators();
    this.thirdFormGroup.controls["idealBusinessCtrl"].updateValueAndValidity();
  }

  addValidators() {
    this.nameFirstStep = "Fill out your data";
    this.nameSecondStep = "Company in Common";
    this.nameThirdStep = "Weaknesses";

    this.secondFormGroup.controls["nameIndustryCtrl"].setValidators(Validators.required);
    this.secondFormGroup.controls["nameIndustryCtrl"].updateValueAndValidity();
    this.secondFormGroup.controls["whyOptimizeCtrl"].setValidators([Validators.required, Validators.maxLength(700)]);
    this.secondFormGroup.controls["whyOptimizeCtrl"].updateValueAndValidity();
    this.toppings.setValidators(Validators.required);
    this.toppings.updateValueAndValidity();

    this.thirdFormGroup.controls["operationalCapabilitiesCtrl"].setValidators([Validators.maxLength(500)]);
    this.thirdFormGroup.controls["operationalCapabilitiesCtrl"].updateValueAndValidity();
    this.thirdFormGroup.controls["indicatorsSatisfyCtrl"].setValidators([Validators.maxLength(500)]);
    this.thirdFormGroup.controls["indicatorsSatisfyCtrl"].updateValueAndValidity();
    this.thirdFormGroup.controls["suitsCompletelyCtrl"].setValidators([Validators.maxLength(500)]);
    this.thirdFormGroup.controls["suitsCompletelyCtrl"].updateValueAndValidity();
    this.thirdFormGroup.controls["idealBusinessCtrl"].setValidators([Validators.maxLength(2000)]);
    this.thirdFormGroup.controls["idealBusinessCtrl"].updateValueAndValidity();
  }

  onSubmit(fullAnswer: boolean) {
    this.fixLastStepIcon();

    let body = `
        <b>About Customer</b><br/><br/>
        <b>Name: </b>${this.firstFormGroup.controls["nameCtrl"].value}<br/> 
        <b>Name company: </b>${this.firstFormGroup.controls["companyCtrl"].value}<br/> 
        <b>Email: </b>${this.firstFormGroup.controls["emailCtrl"].value}<br/> 
        <b>Phone number: </b>${this.firstFormGroup.controls["phoneCtrl"].value}<br/> 
        <b>Skype: </b>${this.firstFormGroup.controls["skypeCtrl"].value}<br/>
        <b>Additional information: </b>${this.firstFormGroup.controls["aboutCtrl"].value}<br/> 
        <br/><br/><b>------------------------------------------------------------------</b><br/><br/>`;

    if (fullAnswer == true) {
      body += `<b>About Company</b><br/><br/>
        <b>Name Industry: </b>${this.secondFormGroup.controls["nameIndustryCtrl"].value}<br/> 
        <b>Most unprofitable process: </b>${this.secondFormGroup.controls["unprofitableProcessCtrl"].value}<br/> 
        <b>Resources are working to their maximum: </b>${this.secondFormGroup.controls["resourcesMaximumCtrl"].value}<br/> 
        <b>Clearly name your weaknesses: </b>${this.secondFormGroup.controls["clearWeaknessesCtrl"].value}<br/> 
        <b>Processes wanted to optimize: </b>${this.secondFormGroup.controls["clearWeaknessesCtrl"].value}<br/> 
        <b>Why want to optimize: </b>${this.toppings.value}<br/>
        <br/><br/><b>------------------------------------------------------------------</b><br/><br/>
        <b>About Weaknesses</b><br/><br/>
        <b>Analyze operational capabilities: </b>${this.thirdFormGroup.controls["operationalCapabilitiesCtrl"].value}<br/> 
        <b>Indicators in business don't satisfy: </b>${this.thirdFormGroup.controls["indicatorsSatisfyCtrl"].value}<br/> 
        <b>Suits completely in business: </b>${this.thirdFormGroup.controls["suitsCompletelyCtrl"].value}<br/> 
        <b>Ideal business with metrics: </b>${this.thirdFormGroup.controls["idealBusinessCtrl"].value}<br/> 
        <b>Idea of improving bad business processes: </b>${this.thirdFormGroup.controls["ideaImproveCtrl"].value}<br/>
    <br/><br/><b>------------------------------------------------------------------</b><br/><br/>`;
    }

    body += `<b>~End of Message.~</b>`;

    Email.send({
      Host: 'smtp.elasticemail.com',
      Username: 'owner@customsolutions.info',
      Password: '3E829C5D585B36117614FD683054A24B3FC5',
      To: 'sales@customsolutions.info',
      From: 'owner@customsolutions.info',
      Subject: 'Audit Potential Customer',
      Body: body
    }).then(message => {
      let answerFromSmtp = message as string;
      if (answerFromSmtp.toLocaleLowerCase() != 'ok') {
        console.error(answerFromSmtp);
      }
    });
  }

  closeModalWindow(stepper: MatStepper) {
    this.modalService.dismissAll();
    this.addValidators();

    if (this.auditText != 'Audit') {
      stepper.reset();
      this.toppings.reset();
    }

    this.isLinear = true;
    this.turnAnimation = false;
    this.textButton = "Next";
    this.auditText = "Audit";
  }

}
