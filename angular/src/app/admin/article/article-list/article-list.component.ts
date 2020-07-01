import { Component, OnInit } from '@angular/core';
import { PaginatedResult } from 'src/app/models/paginated-result';
import { Article } from 'src/app/models/article';
import { ArticleService } from 'src/app/services/article.service';
import { BehaviorSubject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css'],
})
export class ArticleListComponent implements OnInit {
  paginatedResults: PaginatedResult<Article[]>[];
  displayedColumns$ = new BehaviorSubject<string[]>([
    'Id',
    'Picture',
    'Title',
    'Category',
    'Language',
    'Publish Date',
  ]);
  currentPage: number = 1;
  endPage: number = 5;
  retrievedPageCount: number = 5;
  pageChanged$ = new BehaviorSubject<any>({ page: 1, itemsPerPage: 5 });
  pageSize$ = new BehaviorSubject<number>(5);
  dataOnPage$ = new BehaviorSubject<Article[]>([]);
  data$ = new BehaviorSubject<Article[]>([]);
  tableDataSource$ = new BehaviorSubject<Article[]>([]);

  sortKey$ = new BehaviorSubject<string>('publishDate');
  sortDirection$ = new BehaviorSubject<string>('desc');

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    //Load data
    this.route.data.subscribe((dataResult) => {
      this.paginatedResults = dataResult.data;
      if (this.paginatedResults) {
        this.data$.next(
          this.paginatedResults
            .map((x) => x.items)
            .reduce((x, y) => x.concat(y))
        );
      }
    });
    if (!this.paginatedResults) {
      this.articleService.dataResult$.subscribe((dataResult) => {
        this.paginatedResults = dataResult.data;
        this.data$.next(
          this.paginatedResults
            .map((x) => x.items)
            .reduce((x, y) => x.concat(y))
        );
      });
    }

    //Handle sorting
    combineLatest(this.data$, this.sortKey$, this.sortDirection$).subscribe(
      ([articleData, sortKey, sortDirection]) => {
        const sortedArticles = this.nestedSort(
          sortKey,
          articleData,
          sortDirection
        );
        this.tableDataSource$.next(sortedArticles);
      }
    );

    //Handle pagination
    combineLatest(
      this.tableDataSource$,
      this.pageChanged$,
      this.pageSize$
    ).subscribe(([allSources, currentPage, pageSize]) => {
      this.currentPage = currentPage.page;
      const startingIndex =
        this.retrievedPageCount - (this.endPage - this.currentPage) - 1;

      if (this.currentPage > this.endPage) {
        this.handleServerSidePagination(pageSize);
      } else if (this.currentPage <= this.endPage - this.retrievedPageCount) {
        this.handleServerSidePagination(pageSize);
      } else {
        const onPage = allSources.slice(
          startingIndex,
          startingIndex + pageSize
        );
        this.dataOnPage$.next(onPage);
      }
    });
  }

  handleServerSidePagination(pageSize: number) {
    this.endPage = this.currentPage + this.retrievedPageCount - 1;
    this.articleService
      .getArticles(this.currentPage, this.endPage, pageSize)
      .subscribe((dataResult) => {
        this.paginatedResults = dataResult.data;
        this.data$.next(
          this.paginatedResults
            .map((x) => x.items)
            .reduce((x, y) => x.concat(y))
        );
      });
  }

  nestedSort(prop: any, arr: any[], sortDirection: string): any[] {
    prop = prop.split('.');
    const len = prop.length;
    arr.sort((a, b) => {
      let i = 0;
      while (i < len) {
        a = a[prop[i]];
        b = b[prop[i]];
        i++;
      }
      if (a < b) {
        return sortDirection === 'asc' ? -1 : 1;
      } else if (a > b) {
        return sortDirection === 'asc' ? 1 : -1;
      } else {
        return 0;
      }
    });
    return arr;
  }

  adjustSort(key: string) {
    if (this.sortKey$.value === key) {
      if (this.sortDirection$.value === 'asc') {
        this.sortDirection$.next('desc');
      } else {
        this.sortDirection$.next('asc');
      }
      return;
    }

    this.sortKey$.next(key);
    this.sortDirection$.next('asc');
  }
}
