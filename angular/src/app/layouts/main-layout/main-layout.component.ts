import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css'],
})
export class MainLayoutComponent implements OnInit {
  isAsideActive: boolean = true;
  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    if (
      this.router.url.includes('about') ||
      this.router.url.includes('contact')
    ) {
      this.isAsideActive = false;
    } else {
      this.isAsideActive = true;
    }
    this.router.events.subscribe(() => {
      if (
        this.router.url.includes('about') ||
        this.router.url.includes('contact')
      ) {
        this.isAsideActive = false;
      } else {
        this.isAsideActive = true;
      }
    });
  }
}
