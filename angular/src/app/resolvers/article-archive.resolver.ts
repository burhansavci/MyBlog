import { Injectable } from '@angular/core';
import { Article } from '../models/article';
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { ArticleService } from '../services/article.service';
import { AlertifyService } from '../services/alertify.service';
import { catchError, delay, tap } from 'rxjs/operators';
import { ProgressBarService } from '../services/progress-bar.service';

@Injectable({
  providedIn: 'root',
})
export class ArticleArchiveResolver implements Resolve<Article[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private articleService: ArticleService,
    private router: Router,
    private alertify: AlertifyService,
    private progressBar: ProgressBarService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Article[]> {
    this.progressBar.startLoading();
    this.pageNumber = route.paramMap.get('page')
      ? Number(route.paramMap.get('page'))
      : 1;
    const year = Number(route.paramMap.get('year'));

    if (route.paramMap.has('month')) {
      const month = Number(route.paramMap.get('month'));
      return this.articleService
        .getArticlesByYearAndMonth(year, month, this.pageNumber, this.pageSize)
        .pipe(
          delay(1000),
          tap((x) => {
            this.progressBar.completeLoading();
          }),
          catchError((error) => {
            this.alertify.error('Problem retrieving articles data');
            this.router.navigate(['/']);
            return of(null);
          })
        );
    }

    return this.articleService
      .getArticlesByYear(year, this.pageNumber, this.pageSize)
      .pipe(
        delay(1000),
        tap((x) => {
          this.progressBar.completeLoading();
        }),
        catchError((error) => {
          this.alertify.error('Problem retrieving articles data');
          this.router.navigate(['/']);
          return of(null);
        })
      );
  }
}
