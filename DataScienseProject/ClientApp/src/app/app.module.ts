import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { GalleryComponent } from './components/gallery/gallery.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterComponent } from './components/footer/footer.component';
import { TapToTopComponent } from './components/tap-to-top/tap-to-top.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule, MatFormFieldModule, MatIconModule, MatInputModule, MatStepperModule } from '@angular/material';
import { MainPageComponent } from './components/main-page/main-page.component';
import { MainPageDialogComponent } from './components/main-page/main-page-dialog/main-page-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GalleryComponent,
    FooterComponent,
    TapToTopComponent,
    MainPageComponent,
    MainPageDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    MatChipsModule,    
    MatIconModule,
    MatDialogModule,
    MatStepperModule,
    MatFormFieldModule,
    MatInputModule,
    RouterModule.forRoot([
      { path: ':groupName', component: GalleryComponent, pathMatch: 'full' },
      { path: ':groupName/:viewName', component: HomeComponent, pathMatch: 'full' },
      { path: '', component: MainPageComponent, pathMatch: 'full' }
    ]),
    BrowserAnimationsModule
  ],
  entryComponents: [MainPageDialogComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
