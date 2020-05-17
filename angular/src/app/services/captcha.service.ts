import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CaptchaService {
  baseUrl = `${environment.baseUrl}`;
  constructor(private http: HttpClient) {}
  sendToken(token) {
    return this.http.post<any>(`${this.baseUrl}captcha/validate`, { token: token });
  }
}
