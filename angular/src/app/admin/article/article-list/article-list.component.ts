import { Component, OnInit } from '@angular/core';
import { DataResult } from 'src/app/models/data-result';
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
  dataResult: DataResult<Article[]>;
  displayedColumns$ = new BehaviorSubject<string[]>([
    'Id',
    'Picture',
    'Title',
    'Category',
    'Language',
    'Publish Date',
  ]);
  currentPage: number = 1;
  pageChanged$ = new BehaviorSubject<any>({ page: 1, itemsPerPage: 5 });
  pageSize$ = new BehaviorSubject<number>(5);
  dataOnPage$ = new BehaviorSubject<Article[]>([]);
  data$: BehaviorSubject<Article[]>;
  tableDataSource$ = new BehaviorSubject<Article[]>([]);

  sortKey$ = new BehaviorSubject<string>('publishDate');
  sortDirection$ = new BehaviorSubject<string>('desc');

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    //Load data
    this.route.data.subscribe((data) => {
      this.dataResult = data.dataResult;
      if (this.dataResult) {
        this.data$ = new BehaviorSubject<Article[]>(this.dataResult.data);
      }
    });
    if (!this.dataResult) {
      this.articleService.dataResult$.subscribe((dataResult) => {
        this.dataResult = dataResult;
        this.data$ = new BehaviorSubject<Article[]>(this.dataResult.data);
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

    //Handle client-side pagination
    combineLatest(
      this.tableDataSource$,
      this.pageChanged$,
      this.pageSize$
    ).subscribe(([allSources, currentPage, pageSize]) => {
      const startingIndex = (currentPage.page - 1) * pageSize;
      this.currentPage = currentPage.page;
      const onPage = allSources.slice(startingIndex, startingIndex + pageSize);
      this.dataOnPage$.next(onPage);
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
