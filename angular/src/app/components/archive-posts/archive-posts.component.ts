import { Component, OnInit } from '@angular/core';
import { ArticleService } from 'src/app/services/article.service';
import { Archive } from 'src/app/models/archive';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-archive-posts',
  templateUrl: './archive-posts.component.html',
  styleUrls: ['./archive-posts.component.css'],
})
export class ArchivePostsComponent implements OnInit {
  archives: Archive[];
  constructor(
    private articleService: ArticleService,
    private alertify: AlertifyService
  ) {}

  ngOnInit(): void {
    this.articleService.getArticlesArchive().subscribe(
      (data) => {
        this.archives = data.data;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
