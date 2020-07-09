import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { Category } from 'src/app/models/category';
import { PaginatedResult } from 'src/app/models/paginated-result';
import { CategoryService } from 'src/app/services/category.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css'],
})
export class CategoryListComponent implements OnInit {
  paginatedResults: PaginatedResult<Category[]>[];
  displayedColumns$ = new BehaviorSubject<string[]>([
    'Id',
    'Name',
    'Description',
    'Language',
    'Created Date',
  ]);
  currentPage: number = 1;
  endPage: number = 3;
  retrievedPageCount: number = 3;
  pageChanged$ = new BehaviorSubject<any>({ page: 1, itemsPerPage: 5 });
  pageSize$ = new BehaviorSubject<number>(5);
  dataOnPage$ = new BehaviorSubject<Category[]>([]);
  data$ = new BehaviorSubject<Category[]>([]);
  tableDataSource$ = new BehaviorSubject<Category[]>([]);

  sortKey$ = new BehaviorSubject<string>('publishDate');
  sortDirection$ = new BehaviorSubject<string>('desc');

  constructor(
    private categoryService: CategoryService,
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
      this.categoryService.dataResult$.subscribe((dataResult) => {
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
        ((this.currentPage - 1) % this.retrievedPageCount) * pageSize;

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
    this.categoryService
      .getCategories(this.currentPage, this.endPage, pageSize)
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
