import { Component } from '@angular/core';
import {
  Router,
  RouterEvent,
  NavigationStart,
  NavigationEnd,
  NavigationCancel,
  NavigationError,
} from '@angular/router';
import { ProgressBarService } from './services/progress-bar.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'angular';

  constructor(private router: Router, private progressBar: ProgressBarService) {
    this.router.events.subscribe((routerEvent: RouterEvent) => {
      this.navigationInterceptor(routerEvent);
    });
  }

  navigationInterceptor(routerEvent: RouterEvent): void {
    if (routerEvent instanceof NavigationStart) {
      this.progressBar.startLoading();
    }
    if (routerEvent instanceof NavigationEnd) {
      this.progressBar.completeLoading();
    }
    if (routerEvent instanceof NavigationCancel) {
      this.progressBar.setError();
      this.progressBar.completeLoading();
    }
    if (routerEvent instanceof NavigationError) {
      this.progressBar.setError();
      this.progressBar.completeLoading();
    }
  }
}
