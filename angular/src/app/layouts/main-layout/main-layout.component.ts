import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css'],
})
export class MainLayoutComponent implements OnInit {
  isAsideActive: boolean = true;
  constructor(private router: Router) {}

  ngOnInit(): void {
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
