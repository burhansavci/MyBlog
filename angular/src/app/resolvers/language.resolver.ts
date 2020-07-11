import { Injectable } from '@angular/core';
import { Article } from '../models/article';
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { AlertifyService } from '../services/alertify.service';
import { catchError } from 'rxjs/operators';
import { Language } from '../models/language';
import { LanguageService } from '../services/language.service';

@Injectable({
  providedIn: 'root',
})
export class LanguageResolver implements Resolve<Language[]> {
  pageNumber = 1;
  pageSize = 5;
  startPageNumber = 1;
  endPageNumber = 3;

  constructor(
    private languageService: LanguageService,
    private router: Router,
    private alertify: AlertifyService
  ) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Language[]> {
    if (route.paramMap.has('languageCode')) {
      const languageCode = route.paramMap.get('languageCode');
      return this.languageService.getLanguageByCode(languageCode).pipe(
        catchError((error) => {
          this.alertify.error('Problem retrieving language data');
          this.router.navigate(['/']);
          return of(null);
        })
      );
    }

    return this.languageService
      .getPaginatedLanguages(
        this.startPageNumber,
        this.endPageNumber,
        this.pageSize
      )
      .pipe(
        catchError(() => {
          this.alertify.error('Problem retrieving languages data');
          this.router.navigate(['/']);
          return of(null);
        })
      );
  }
}
