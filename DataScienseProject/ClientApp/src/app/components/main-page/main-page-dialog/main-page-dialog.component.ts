import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from '../main-page.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MainPageService } from '../../../services/main-page.service';
import { TagModel } from '../../../models/main-page/tag.model';
import { MatChip, MatStepper } from '@angular/material';
import { DataToSendModel } from '../../../models/main-page/data-to-send';
import { TagResModel } from 'src/app/models/main-page/tagRes.model';

@Component({
  selector: 'app-main-page-dialog',
  templateUrl: './main-page-dialog.component.html',
  styleUrls: ['./main-page-dialog.component.css']
})
export class MainPageDialogComponent implements OnInit {
  isLinear = false;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  allTags: Array<TagResModel>;
  tag: TagModel;
  selectedTagsList: TagModel[] = [];
  isTagsSelected: boolean = false;
  isInputFill: boolean = false;

  constructor(public dialogRef: MatDialogRef<MainPageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData, private _formBuilder: FormBuilder, private mainPageService: MainPageService) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      emailCtrl: ['', [Validators.required, Validators.email]],
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required],
    });
    this.getTags();
  }

  ngDoCheck() {
    if (this.firstFormGroup.valid) {
      this.isInputFill = true;
    } else {
      this.isInputFill = false;
    }
  }

  getTags() {
    this.mainPageService.getTags().subscribe((data) => {
      this.allTags = data;
    },
      error => { console.error('There was an error!', error); });
  }

  tagsSelection(chip: MatChip, index: number) {
    chip.toggleSelected();
    if (chip.selected) {
      this.selectedTagsList.push(chip.value);
    } else {
      this.selectedTagsList.forEach((element, index) => {
        if (element == chip.value) this.selectedTagsList.splice(index, 1);
      });
    }
    this.selectedTagsList.length > 0 ? this.isTagsSelected = true : this.isTagsSelected = false;
  }

  sendTags() {
    if (this.isTagsSelected) {
      var dataToSendModel: DataToSendModel = new DataToSendModel();
      dataToSendModel.email = this.firstFormGroup.controls['emailCtrl'].value;
      dataToSendModel.tagsList = this.selectedTagsList;
      this.mainPageService.sendTags(dataToSendModel).subscribe(() => {
      },
        error => { console.error('There was an error!', error); });
    }

  }

  close(): void {
    this.dialogRef.close();
  }

}
