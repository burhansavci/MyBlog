import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Language } from '../models/language';
import { Observable, BehaviorSubject } from 'rxjs';
import { DataResult } from '../models/data-result';
import { PaginatedResult } from '../models/paginated-result';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class LanguageService {
  baseUrl = `${environment.baseUrl}languages`;
  loading: boolean = false;
  private dateResultSource = new BehaviorSubject<
    DataResult<PaginatedResult<Language[]>[]>
  >(null);
  dataResult$ = this.dateResultSource.asObservable();

  constructor(private http: HttpClient) {}

  getLanguages(): Observable<DataResult<Language[]>> {
    return this.http.get<DataResult<Language[]>>(this.baseUrl);
  }
  getPaginatedLanguages(
    startPageNumber?: number,
    endPageNumber?: number,
    pageSize?: number
  ): Observable<DataResult<PaginatedResult<Language[]>[]>> {
    const url = `${this.baseUrl}/${startPageNumber}/${endPageNumber}/${pageSize}`;
    return this.http.get<DataResult<PaginatedResult<Language[]>[]>>(url).pipe(
      map((dataResult: DataResult<PaginatedResult<Language[]>[]>) => {
        this.dateResultSource.next(dataResult);
        return dataResult;
      })
    );
  }

  getLanguageByCode(languageCode: string) {
    return this.http.get<DataResult<Language>>(
      `${this.baseUrl}/${languageCode}`
    );
  }
}
