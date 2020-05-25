import { Component, OnInit, ViewChild } from '@angular/core';
import { ArticleService } from 'src/app/services/article.service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent implements OnInit {
  render: boolean = false;

  constructor(private articleService: ArticleService) {
    this.articleService.getArticles().subscribe((dataResult) => {
      this.render = true;
    });
  }

  ngOnInit(): void {}
}
