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
import { MatIconModule } from '@angular/material';
import { MainPageComponent } from './components/main-page/main-page.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GalleryComponent,
    FooterComponent,
    TapToTopComponent,
    MainPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    MatChipsModule,    
    MatIconModule,
    RouterModule.forRoot([
      { path: ':groupName', component: GalleryComponent, pathMatch: 'full' },
      { path: ':groupName/:viewName', component: HomeComponent, pathMatch: 'full' },
      { path: '', component: MainPageComponent, pathMatch: 'full' }
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
