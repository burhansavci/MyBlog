import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { MainNavComponent } from './nav/main-nav/main-nav.component';
import { FooterNavComponent } from './nav/footer-nav/footer-nav.component';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    MainNavComponent,
    FooterNavComponent,
  ],
  imports: [BrowserModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
