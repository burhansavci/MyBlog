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
@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css'],
})
export class AdminLayoutComponent implements OnInit, AfterViewInit {
  activeClass = 'active';
  pageTitle: string = 'Home';
  activeEl: ElementRef<any>;
  @ViewChildren(RouterLinkActive, { read: ElementRef })
  linkRefs: QueryList<ElementRef>;

  constructor(private location: Location) {
    this.location.onUrlChange((url) => {
      this.adjustPageTitle();
    });
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.adjustPageTitle();
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
