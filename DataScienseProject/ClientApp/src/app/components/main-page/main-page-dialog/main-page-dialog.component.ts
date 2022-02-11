import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from '../main-page.component';

@Component({
  selector: 'app-main-page-dialog',
  templateUrl: './main-page-dialog.component.html',
  styleUrls: ['./main-page-dialog.component.css']
})
export class MainPageDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<MainPageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,) { }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
