import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Article } from '../models/article';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/paginated-result';
import { DataResult } from '../models/data-result';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  baseUrl = `${environment.baseUrl}tr/articles/`;

  constructor(private http: HttpClient) {}

  getArticleById(id: number) {
    return this.http.get<Article>(this.baseUrl + id);
  }

  getArticles(
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
}