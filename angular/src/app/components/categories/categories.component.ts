import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  categories: Category[];
  constructor(
    private categoryService: CategoryService,
    private alertify: AlertifyService
  ) {}

  ngOnInit(): void {
    this.categoryService.getCategoriesByLanguageCode().subscribe(
      (data) => {
        this.categories = data.data;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
