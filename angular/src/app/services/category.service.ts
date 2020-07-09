import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { DataResult } from '../models/data-result';
import { Category } from '../models/category';
import { PaginatedResult } from '../models/paginated-result';
import { map, tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baseUrl = `${environment.baseUrl}tr/categories`;
  loading: boolean = false;
  private dateResultSource = new BehaviorSubject<
    DataResult<PaginatedResult<Category[]>[]>
  >(null);
  dataResult$ = this.dateResultSource.asObservable();

  constructor(private http: HttpClient) {}

  getCategories(
    startPageNumber?: number,
    endPageNumber?: number,
    pageSize?: number
  ): Observable<DataResult<PaginatedResult<Category[]>[]>> {
    const url = `${environment.baseUrl}categories/${startPageNumber}/${endPageNumber}/${pageSize}`;
    return this.http.get<DataResult<PaginatedResult<Category[]>[]>>(url).pipe(
      map((dataResult: DataResult<PaginatedResult<Category[]>[]>) => {
        this.dateResultSource.next(dataResult);
        return dataResult;
      })
    );
  }
  getCategoryById(id: number) {
    return this.http.get<DataResult<Category>>(
      `${environment.baseUrl}categories/${id}`
    );
  }

  getCategoriesByLanguageCode(): Observable<DataResult<Category[]>> {
    return this.http.get<DataResult<Category[]>>(this.baseUrl);
  }

  addCategory(category: Category) {
    this.loading = true;
    const url = `${environment.baseUrl}categories`;
    let headers = new HttpHeaders();
    headers = headers.set(
      'Authorization',
      `Bearer ${localStorage.getItem('token')}`
    );
    return this.http.post(url, category, { headers }).pipe(
      tap((x) => {
        this.loading = false;
      }),
      catchError((error) => {
        this.loading = false;
        return of(null);
      })
    );
  }

  updateCategory(category: Category) {
    this.loading = true;
    const url = `${environment.baseUrl}categories`;
    let headers = new HttpHeaders();
    headers = headers.set(
      'Authorization',
      `Bearer ${localStorage.getItem('token')}`
    );
    return this.http.put(url, category, { headers }).pipe(
      tap((x) => {
        this.loading = false;
      }),
      catchError((error) => {
        this.loading = false;
        return of(null);
      })
    );
  }

  deleteCategory(category: Category) {
    this.loading = true;
    const url = `${environment.baseUrl}categories`;

    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      }),
      body: category,
    };

    return this.http.delete(url, options).pipe(
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
