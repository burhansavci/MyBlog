import { Injectable } from '@angular/core';
import { NgProgressRef, NgProgress } from 'ngx-progressbar';

@Injectable({
  providedIn: 'root',
})
export class ProgressBarService {
  progressRef: NgProgressRef;

  constructor(private progress: NgProgress) {
    this.progressRef = progress.ref('progressBar');
  }

  startLoading() {
    this.progressRef.start();
  }
  completeLoading() {
    this.progressRef.complete();
  }
}
