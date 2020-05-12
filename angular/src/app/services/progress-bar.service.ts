import { Injectable } from '@angular/core';
import { NgProgressRef, NgProgress } from 'ngx-progressbar';

@Injectable({
  providedIn: 'root',
})
export class ProgressBarService {
  progressRef: NgProgressRef;
  defaultColor: string = '#1B95E0';
  successColor: string = '#42A948';
  errorColor: string = '#a94442';
  currentColor: string = this.defaultColor;

  constructor(private progress: NgProgress) {
    this.progressRef = this.progress.ref('progressBar');
  }

  startLoading() {
    this.currentColor = this.defaultColor;
    this.progressRef.start();
  }

  completeLoading() {
    this.progressRef.complete();
  }

  incrementLoading(n: number) {
    this.progressRef.inc(n);
  }

  setLoading(n: number) {
    this.progressRef.set(n);
  }

  setSuccess() {
    this.currentColor = this.successColor;
  }

  setError() {
    this.currentColor = this.errorColor;
  }
}
