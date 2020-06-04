import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Archive } from '../models/archive';
import { Article } from '../models/article';
import { DataResult } from '../models/data-result';
import { PaginatedResult } from '../models/paginated-result';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  baseUrl = `${environment.baseUrl}tr/articles/`;
  loading: boolean = false;
  private dateResultSource = new BehaviorSubject<DataResult<Article[]>>(null);
  dataResult$ = this.dateResultSource.asObservable();
  constructor(private http: HttpClient) {}

  getArticleById(id: number) {
    return this.http.get<DataResult<Article>>(this.baseUrl + id);
  }

  getArticles() {
    const url = `${environment.baseUrl}articles`;
    return this.http.get<DataResult<Article[]>>(url).pipe(
      map((dataResult: DataResult<Article[]>) => {
        this.dateResultSource.next(dataResult);
        return dataResult;
      })
    );
  }

  getArticlesByLanguageCode(
    pageNumber?: number,
    pageSize?: number
  ): Observable<DataResult<PaginatedResult<Article[]>>> {
    const url = `${this.baseUrl}${pageNumber}/${pageSize}`;
    return this.http.get<DataResult<PaginatedResult<Article[]>>>(url);
  }

  getArticlesByCategoryId(
    categoryId: number,
    pageNumber?: number,
    pageSize?: number
  ): Observable<PaginatedResult<Article[]>> {
    const url = `${this.baseUrl}${categoryId}/${pageNumber}/${pageSize}`;
    return this.http.get<PaginatedResult<Article[]>>(url);
  }

  getArticlesArchive(): Observable<DataResult<Archive[]>> {
    const url = `${this.baseUrl}archive`;
    return this.http.get<DataResult<Archive[]>>(url);
  }

  getArticlesByYear(
    year: number,
    pageNumber?: number,
    pageSize?: number
  ): Observable<DataResult<Article[]>> {
    const url = `${this.baseUrl}archive/${year}/${pageNumber}/${pageSize}`;
    return this.http.get<DataResult<Article[]>>(url);
  }

  getArticlesByYearAndMonth(
    year: number,
    month: number,
    pageNumber?: number,
    pageSize?: number
  ): Observable<DataResult<Article[]>> {
    const url = `${this.baseUrl}archive/${year}/${month}/${pageNumber}/${pageSize}`;
    return this.http.get<DataResult<Article[]>>(url);
  }

  addArticle(article: any) {
    this.loading = true;
    const url = `${environment.baseUrl}articles`;
    let headers = new HttpHeaders();
    headers = headers.set(
      'Authorization',
      `Bearer ${localStorage.getItem('token')}`
    );
    return this.http.post(url, article, { headers }).pipe(
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
