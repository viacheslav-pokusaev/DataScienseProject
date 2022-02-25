import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPageDialogComponent } from './main-page-dialog.component';

describe('MainPageDialogComponent', () => {
  let component: MainPageDialogComponent;
  let fixture: ComponentFixture<MainPageDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainPageDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainPageDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
