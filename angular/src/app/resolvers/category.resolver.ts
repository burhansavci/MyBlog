import { Injectable } from '@angular/core';
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { AlertifyService } from '../services/alertify.service';
import { catchError } from 'rxjs/operators';
import { Category } from '../models/category';
import { CategoryService } from '../services/category.service';

@Injectable({
  providedIn: 'root',
})
export class CategoryResolver implements Resolve<Category[]> {
  pageNumber = 1;
  pageSize = 5;
  startPageNumber = 1;
  endPageNumber = 3;

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Category[]> {

    if (route.paramMap.has('id')) {
      const id = Number(route.paramMap.get('id'));
      return this.categoryService.getCategoryById(id).pipe(
        catchError((error) => {
          this.alertify.error('Problem retrieving category data');
          this.router.navigate(['/']);
          return of(null);
        })
      );
    }

    return this.categoryService
      .getCategories(this.startPageNumber, this.endPageNumber, this.pageSize)
      .pipe(
        catchError(() => {
          this.alertify.error('Problem retrieving categories data');
          this.router.navigate(['/']);
          return of(null);
        })
      );
  }
}
