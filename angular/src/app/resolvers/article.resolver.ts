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
export class ArticleResolver implements Resolve<Article[]> {
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
    this.pageNumber = route.paramMap.get('page')
      ? Number(route.paramMap.get('page'))
      : 1;
    if (route.paramMap.has('title')) {
      const id = Number(route.paramMap.get('id'));
      return this.articleService.getArticleById(id).pipe(
        catchError((error) => {
          this.alertify.error('Problem retrieving article data');
          this.router.navigate(['/']);
          return of(null);
        })
      );
    }

    return this.articleService.getArticles(this.pageNumber, this.pageSize).pipe(
      catchError((error) => {
        this.alertify.error('Problem retrieving articles data');
        this.router.navigate(['/']);
        return of(null);
      })
    );
  }
}
