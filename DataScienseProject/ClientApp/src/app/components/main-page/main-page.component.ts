import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MainPageService } from '../../services/main-page.service';
import { MainPageDialogComponent } from './main-page-dialog/main-page-dialog.component';


export interface DialogData {
  animal: string;
  name: string;
}

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {  

  constructor(public dialog: MatDialog) { }


  openDialog(): void {
    const dialogRef = this.dialog.open(MainPageDialogComponent, {
      
      
    });

    //dialogRef.afterClosed().subscribe(result => {
    //  console.log('The dialog was closed');
    //  this.animal = result;
    //});
  }


  ngOnInit() {
  }

}


