import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { DataResult } from '../models/data-result';
import { Category } from '../models/category';
import { PaginatedResult } from '../models/paginated-result';
import { map } from 'rxjs/operators';

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

  getCategoriesByLanguageCode(): Observable<DataResult<Category[]>> {
    return this.http.get<DataResult<Category[]>>(this.baseUrl);
  }
}
