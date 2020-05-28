import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Language } from '../models/language';
import { Observable } from 'rxjs';
import { DataResult } from '../models/data-result';

@Injectable({
  providedIn: 'root',
})
export class LanguageService {
  baseUrl = `${environment.baseUrl}languages`;

  constructor(private http: HttpClient) {}

  getLanguages(): Observable<DataResult<Language[]>> {
    return this.http.get<DataResult<Language[]>>(this.baseUrl);
  }
}
