import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { of, Observable } from 'rxjs';
import { Admin } from '../models/admin';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = `${environment.baseUrl}auth/`;

  constructor(
    private http: HttpClient,
    private router: Router,
    public jwtHelper: JwtHelperService
  ) {}

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  loadCurrentUser(token: string): Observable<Admin> {
    if (token === null) {
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(this.baseUrl, { headers }).pipe(
      map((admin: Admin) => {
        localStorage.setItem('token', admin.token);
        localStorage.setItem('userId', admin.id.toString());
        return admin;
      })
    );
  }

  login(admin: Admin) {
    return this.http.post(`${this.baseUrl}login`, admin).pipe(
      map((admin: Admin) => {
        localStorage.setItem('token', admin.token);
        localStorage.setItem('userId', admin.id.toString());
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigateByUrl('/');
  }
}
