import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { AccordionModule } from 'ngx-bootstrap/accordion';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { MainNavComponent } from './nav/main-nav/main-nav.component';
import { FooterNavComponent } from './nav/footer-nav/footer-nav.component';
import { AboutComponent } from './pages/about/about.component';
import { ContactComponent } from './pages/contact/contact.component';
import { HomeComponent } from './pages/home/home.component';
import { ArticleService } from './services/article.service';
import { HttpClientModule } from '@angular/common/http';
import { ArticleComponent } from './pages/article/article.component';
import { UrlFormatPipe } from './pipes/url-format.pipe';
import { RecentPostsComponent } from './components/recent-posts/recent-posts.component';
import { AlertifyService } from './services/alertify.service';
import { ArchivePostsComponent } from './components/archive-posts/archive-posts.component';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    MainNavComponent,
    FooterNavComponent,
    AboutComponent,
    ContactComponent,
    HomeComponent,
    ArticleComponent,
    UrlFormatPipe,
    RecentPostsComponent,
    ArchivePostsComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    ButtonsModule.forRoot(),
    PaginationModule.forRoot(),
    CollapseModule.forRoot(),
    AccordionModule.forRoot(),
    FormsModule,
  ],
  providers: [ArticleService, AlertifyService],
  bootstrap: [AppComponent],
})
export class AppModule {}
