import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProgressBarService } from '../services/progress-bar.service';
import { delay, finalize } from 'rxjs/operators';

@Injectable()
export class ProgressBarInterceptor implements HttpInterceptor {
  constructor(private progressBarService: ProgressBarService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.progressBarService.startLoading();

    return next.handle(request).pipe(
      delay(1000), //for development
      finalize(() => {
        this.progressBarService.completeLoading();
      })
    );
  }
}
