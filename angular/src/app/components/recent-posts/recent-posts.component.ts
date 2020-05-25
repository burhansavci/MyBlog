import { Component, OnInit, Input } from '@angular/core';
import { DataResult } from 'src/app/models/data-result';
import { PaginatedResult } from 'src/app/models/paginated-result';
import { Article } from 'src/app/models/article';
import { ArticleService } from 'src/app/services/article.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-recent-posts',
  templateUrl: './recent-posts.component.html',
  styleUrls: ['./recent-posts.component.css'],
})
export class RecentPostsComponent implements OnInit {
  dataResult: DataResult<PaginatedResult<Article[]>>;
  constructor(
    private articleService: ArticleService,
    private alertify: AlertifyService
  ) {}

  ngOnInit(): void {
    this.loadRecentPosts();
  }
  loadRecentPosts() {
    this.articleService.getArticlesByLanguageCode(1, 5).subscribe(
      (dataResult: DataResult<PaginatedResult<Article[]>>) => {
        this.dataResult = dataResult;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
