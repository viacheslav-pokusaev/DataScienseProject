import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from '../main-page.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MainPageService } from '../../../services/main-page.service';
import { TagModel } from '../../../models/main-page/tag.model';
import { MatChip } from '@angular/material';



@Component({
  selector: 'app-main-page-dialog',
  templateUrl: './main-page-dialog.component.html',
  styleUrls: ['./main-page-dialog.component.css']
})
export class MainPageDialogComponent implements OnInit {
  isLinear = false;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  allTags: Array<TagModel>;
  tag: TagModel;
  selectedTagsList: TagModel[] = [];

  constructor(public dialogRef: MatDialogRef<MainPageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData, private _formBuilder: FormBuilder, private mainPageService: MainPageService) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      emailCtrl: ['', [Validators.required, Validators.email]],
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required],
    });
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
  }

  sendTags() {
    this.mainPageService.sendTags(this.selectedTagsList).subscribe(() => {
      console.log("Work");
    },
      error => { console.error('There was an error!', error); });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
