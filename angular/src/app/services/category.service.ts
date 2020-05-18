import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DataResult } from '../models/data-result';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baseUrl = `${environment.baseUrl}tr/categories`;

  constructor(private http: HttpClient) {}

  getCategories(): Observable<DataResult<Category[]>> {
    return this.http.get<DataResult<Category[]>>(this.baseUrl);
  }
}
