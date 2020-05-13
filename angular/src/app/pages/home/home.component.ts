import { Component, OnInit } from '@angular/core';
import { PaginatedResult } from 'src/app/models/paginated-result';
import { Article } from 'src/app/models/article';
import { ActivatedRoute, Router } from '@angular/router';
import { DataResult } from 'src/app/models/data-result';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  dataResult: DataResult<PaginatedResult<Article[]>>;
  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.dataResult = data.dataResult;
    });
  }

  pageChanged(event: any): void {
    if (this.route.snapshot.paramMap.has('categoryName')) {
      let categoryName = this.route.snapshot.paramMap.get('categoryName');
      let categoryId = this.route.snapshot.paramMap.get('categoryId');
      this.router.navigate([
        `/category/${categoryName}/${categoryId}/page/${event.page}`,
      ]);
    } else if (this.route.snapshot.paramMap.has('month')) {
      let year = this.route.snapshot.paramMap.get('year');
      let month = this.route.snapshot.paramMap.get('month');
      this.router.navigate([`/archive/${year}/${month}/page/${event.page}`]);
    } else if (this.route.snapshot.paramMap.has('year')) {
      let year = this.route.snapshot.paramMap.get('year');
      this.router.navigate([`/archive/${year}/page/${event.page}`]);
    } else {
      this.router.navigate([`/page/${event.page}`], { relativeTo: this.route });
    }
  }
}
