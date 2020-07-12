import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Language } from '../models/language';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { DataResult } from '../models/data-result';
import { PaginatedResult } from '../models/paginated-result';
import { map, tap, catchError } from 'rxjs/operators';

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

  addLanguage(language: Language){
    this.loading = true;
    let headers = new HttpHeaders();
    headers = headers.set(
      'Authorization',
      `Bearer ${localStorage.getItem('token')}`
    );
    return this.http.post(this.baseUrl, language, { headers }).pipe(
      tap((x) => {
        this.loading = false;
      }),
      catchError((error) => {
        this.loading = false;
        return of(null);
      })
    );
  }

  updateLanguage(language: Language) {
    this.loading = true;
    let headers = new HttpHeaders();
    headers = headers.set(
      'Authorization',
      `Bearer ${localStorage.getItem('token')}`
    );
    return this.http.put(this.baseUrl, language, { headers }).pipe(
      tap((x) => {
        this.loading = false;
      }),
      catchError((error) => {
        this.loading = false;
        return of(null);
      })
    );
  }

  deleteLanguage(language: Language) {
    this.loading = true;

    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      }),
      body: language,
    };

    return this.http.delete(this.baseUrl, options).pipe(
      tap((x) => {
        this.loading = false;
      }),
      catchError((error) => {
        this.loading = false;
        return of(null);
      })
    );
  }
}
