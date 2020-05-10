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
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ArticleArchiveResolver implements Resolve<Article[]> {
  constructor(
    private articleService: ArticleService,
    private router: Router,
    private alertify: AlertifyService
  ) {}
  pageNumber = 1;
  pageSize = 5;
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Article[]> {
    console.log(Number(route.paramMap.get('page')));
    this.pageNumber = route.paramMap.get('page')
      ? Number(route.paramMap.get('page'))
      : 1;
    let year = Number(route.paramMap.get('year'));
    if (route.paramMap.has('month')) {
      let month = Number(route.paramMap.get('month'));
      return this.articleService
        .getArticlesByYearAndMonth(year, month, this.pageNumber, this.pageSize)
        .pipe(
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
        catchError((error) => {
          this.alertify.error('Problem retrieving articles data');
          this.router.navigate(['/']);
          return of(null);
        })
      );
  }
}
