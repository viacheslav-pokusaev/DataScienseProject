import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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
export class MainPageComponent {  

  constructor(public dialog: MatDialog) { }

  openDialog(): void {
    const dialogRef = this.dialog.open(MainPageDialogComponent, {
      width: '1400px',
      disableClose: true      
    });
  }

}


