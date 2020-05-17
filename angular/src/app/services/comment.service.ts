import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';
import { DataResult } from '../models/data-result';
import { Comment } from '../models/comment';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  baseUrl = `${environment.baseUrl}comments`;
  loading: boolean;
  constructor(private http: HttpClient) {}

  getComments(): Observable<DataResult<Comment[]>> {
    return this.http.get<DataResult<Comment[]>>(this.baseUrl);
  }

  getCommentsByArticleId(articleId: number): Observable<DataResult<Comment[]>> {
    const url = `${this.baseUrl}/${articleId}`;
    return this.http.get<DataResult<Comment[]>>(url);
  }

  addComment(comment: Comment) {
    this.loading = true;
    return this.http.post<Comment>(this.baseUrl, comment).pipe(
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
