import { Component, OnInit } from '@angular/core';
import { PaginatedResult } from 'src/app/models/paginated-result';
import { Language } from 'src/app/models/language';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { LanguageService } from 'src/app/services/language.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-language-list',
  templateUrl: './language-list.component.html',
  styleUrls: ['./language-list.component.css'],
})
export class LanguageListComponent implements OnInit {
  paginatedResults: PaginatedResult<Language[]>[];
  displayedColumns$ = new BehaviorSubject<string[]>([
    'LanguageCode',
    'Name',
    'Default',
  ]);
  currentPage: number = 1;
  endPage: number = 3;
  retrievedPageCount: number = 3;
  pageChanged$ = new BehaviorSubject<any>({ page: 1, itemsPerPage: 5 });
  pageSize$ = new BehaviorSubject<number>(5);
  dataOnPage$ = new BehaviorSubject<Language[]>([]);
  data$ = new BehaviorSubject<Language[]>([]);
  tableDataSource$ = new BehaviorSubject<Language[]>([]);

  sortKey$ = new BehaviorSubject<string>('isDefault');
  sortDirection$ = new BehaviorSubject<string>('desc');
  constructor(
    private languageService: LanguageService,
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
      this.languageService.dataResult$.subscribe((dataResult) => {
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
    const startPage =
      this.currentPage - ((this.currentPage - 1) % this.retrievedPageCount);
    this.endPage = startPage + this.retrievedPageCount - 1;
    this.languageService
      .getPaginatedLanguages(this.currentPage, this.endPage, pageSize)
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
