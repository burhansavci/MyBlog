import {
  Component,
  OnInit,
  ViewChildren,
  ElementRef,
  QueryList,
  AfterViewInit,
} from '@angular/core';
import { RouterLinkActive } from '@angular/router';
import { Location } from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { Admin } from 'src/app/models/admin';
@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css'],
})
export class AdminLayoutComponent implements OnInit, AfterViewInit {
  admin: Admin;
  activeClass = 'active';
  pageTitle: string = 'Home';
  isSidebarOpen: boolean = window.innerWidth < 768 ? false : true;
  activeEl: ElementRef<any>;
  @ViewChildren(RouterLinkActive, { read: ElementRef })
  linkRefs: QueryList<ElementRef>;

  constructor(
    private location: Location,
    private authService: AuthService,
    private alertifyService: AlertifyService
  ) {
    this.location.onUrlChange((url) => {
      this.adjustPageTitle();
    });
  }

  ngOnInit(): void {
    this.authService.loadCurrentUser(localStorage.getItem('token')).subscribe(
      (admin: Admin) => {
        this.admin = admin;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }

  ngAfterViewInit(): void {
    this.adjustPageTitle();
  }

  toggle(isSidebarOpen: boolean) {
    this.isSidebarOpen = isSidebarOpen;
  }

  adjustPageTitle(): void {
    setTimeout(() => {
      this.activeEl = this.linkRefs
        .toArray()
        .find((e) => e.nativeElement.classList.contains(this.activeClass));
      if (this.activeEl) {
        this.pageTitle = this.activeEl.nativeElement.innerText;
      }
    }, 0);
  }
}
