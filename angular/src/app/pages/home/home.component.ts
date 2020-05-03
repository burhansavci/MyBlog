import { Component, OnInit } from '@angular/core';
import { PaginatedResult } from 'src/app/models/paginated-result';
import { Article } from 'src/app/models/article';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from 'src/app/services/article.service';
import { DataResult } from 'src/app/models/data-result';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  dataResult: DataResult<PaginatedResult<Article[]>>;
  constructor(
    private articleService: ArticleService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.dataResult = data.dataResult;
    });
  }

  pageChanged(event: any): void {
    this.router.navigate([`/page/${event.page}`]);
    window.scrollTo(0, 0);
  }
}
