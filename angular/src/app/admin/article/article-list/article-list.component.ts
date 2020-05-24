import { Component, OnInit } from '@angular/core';
import { DataResult } from 'src/app/models/data-result';
import { PaginatedResult } from 'src/app/models/paginated-result';
import { Article } from 'src/app/models/article';
import { ArticleService } from 'src/app/services/article.service';
import { BehaviorSubject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css'],
})
export class ArticleListComponent implements OnInit {
  dataResult: DataResult<PaginatedResult<Article[]>>;
  displayedColumns$ = new BehaviorSubject<string[]>([
    'Id',
    'Picture',
    'Title',
    'Category',
    'Language',
    'Publish Date',
  ]);
  pageChanged$ = new BehaviorSubject<any>({ page: 1, itemsPerPage: 5 });
  pageSize$ = new BehaviorSubject<number>(5);
  dataOnPage$ = new BehaviorSubject<Article[]>([]);
  data$: BehaviorSubject<Article[]>;
  tableDataSource$ = new BehaviorSubject<Article[]>([]);

  sortKey$ = new BehaviorSubject<string>('publishDate');
  sortDirection$ = new BehaviorSubject<string>('desc');
  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    // this.articleService.getArticles(1, 10).subscribe((dataResult) => {
    //   this.dataResult = dataResult;
    // });
    this.route.data.subscribe((data) => {
      this.dataResult = data.dataResult;
      this.data$ = new BehaviorSubject<Article[]>(this.dataResult.data.items);
    });

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
      this.dataResult.data.currentPage = currentPage.page;
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
  // pageChanged(event: any): void {
  //   const startingIndex = (event.page - 1) * 5;
  //   const onPage = this.dataResult.data.items.slice(
  //     startingIndex,
  //     startingIndex + 5
  //   );
  //   this.dataOnPage$.next(onPage);
  // }

  //
  // heroes$ = new BehaviorSubject<{[name: string]: any}>({})

  // superlatives$ = new BehaviorSubject<{[superlativeName: string]: string}>({});
  // tableDataSource$ = new BehaviorSubject<any[]>([]);
  // displayedColumns$ = new BehaviorSubject<string[]>([
  //   'name',
  //   'types',
  //   'attack',
  //   'defense',
  //   'speed',
  //   'healing',
  //   'recovery',
  //   'health',
  //   'levelUp'
  // ]);
  // currentPage$ = new BehaviorSubject<number>(1);
  // pageSize$ = new BehaviorSubject<number>(5);
  // dataOnPage$ = new BehaviorSubject<any[]>([]);
  // searchFormControl = new FormControl();
  // sortKey$ = new BehaviorSubject<string>('name');
  // sortDirection$ = new BehaviorSubject<string>('asc');

  // constructor() { }

  // ngOnInit() {
  //   this.heroes$.subscribe(changedHeroData => {
  //     const superlatives = {
  //       'highest-attack': null,
  //       'lowest-attack': null,
  //       'highest-defense': null,
  //       'lowest-defense': null,
  //       'highest-speed': null,
  //       'lowest-speed': null,
  //       'highest-healing': null,
  //       'lowest-healing': null,
  //       'highest-recovery': null,
  //       'lowest-recovery': null,
  //       'highest-health': null,
  //       'lowest-health': null
  //     };

  //     Object.values(changedHeroData).forEach(hero => {
  //       Object.keys(hero).forEach(key => {
  //         if (key === 'name' || key === 'types') { return; }

  //         const highest = `highest-${key}`;
  //         if (!superlatives[highest] || hero[key] > changedHeroData[superlatives[highest]][key]) {
  //           superlatives[highest] = hero.name;
  //         }

  //         const lowest = `lowest-${key}`;
  //         if (!superlatives[lowest] || hero[key] < changedHeroData[superlatives[lowest]][key]) {
  //           superlatives[lowest] = hero.name;
  //         }
  //       });
  //     });

  //     this.superlatives$.next(superlatives);
  //   });

  //   combineLatest(this.tableDataSource$, this.currentPage$, this.pageSize$)
  //   .subscribe(([allSources, currentPage, pageSize]) => {
  //     const startingIndex = (currentPage - 1) * pageSize;
  //     const onPage = allSources.slice(startingIndex, startingIndex + pageSize);
  //     this.dataOnPage$.next(onPage);
  //   });

  //   this.heroes$.pipe(take(1)).subscribe(heroData => {
  //     this.tableDataSource$.next(Object.values(heroData));
  //   });

  //   combineLatest(this.heroes$, this.searchFormControl.valueChanges, this.sortKey$, this.sortDirection$)
  //   .subscribe(([changedHeroData, searchTerm, sortKey, sortDirection]) => {
  //     const heroesArray = Object.values(changedHeroData);
  //     let filteredHeroes: any[];

  //     if (!searchTerm) {
  //       filteredHeroes = heroesArray;
  //     } else {
  //       const filteredResults = heroesArray.filter(hero => {
  //         return Object.values(hero)
  //           .reduce((prev, curr) => {
  //             return prev || curr.toString().toLowerCase().includes(searchTerm.toLowerCase());
  //           }, false);
  //       });
  //       filteredHeroes = filteredResults;
  //     }

  //     const sortedHeroes = filteredHeroes.sort((a, b) => {
  //       if(a[sortKey] > b[sortKey]) return sortDirection === 'asc' ? 1 : -1;
  //       if(a[sortKey] < b[sortKey]) return sortDirection === 'asc' ? -1 : 1;
  //       return 0;
  //     });

  //     this.tableDataSource$.next(sortedHeroes);
  //   });

  //   this.searchFormControl.setValue('');
  // }

  // adjustSort(key: string) {
  //   if (this.sortKey$.value === key) {
  //     if (this.sortDirection$.value === 'asc') {
  //       this.sortDirection$.next('desc');
  //     } else {
  //       this.sortDirection$.next('asc');
  //     }
  //     return;
  //   }

  //   this.sortKey$.next(key);
  //   this.sortDirection$.next('asc');
  // }

  // levelUp(heroName: string) {
  //   const updatedHero = { ... this.heroes$.value[heroName] };
  //   updatedHero.attack = Math.round(updatedHero.attack * (1 + (Math.random() / 8)));
  //   updatedHero.defense = Math.round(updatedHero.defense * (1 + (Math.random() / 8)));
  //   updatedHero.speed = Math.round(updatedHero.speed * (1 + (Math.random() / 8)));
  //   updatedHero.recovery = Math.round(updatedHero.recovery * (1 + (Math.random() / 8)));
  //   updatedHero.healing = Math.round(updatedHero.healing * (1 + (Math.random() / 8)));
  //   updatedHero.health = Math.round(updatedHero.health * (1 + (Math.random() / 8)));

  //   const newHeroData = { ... this.heroes$.value };
  //   newHeroData[heroName] = updatedHero;

  //   this.heroes$.next(newHeroData);
  // }
}
