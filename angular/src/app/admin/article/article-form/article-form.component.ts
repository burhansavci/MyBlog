import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';

@Component({
  selector: 'app-article-form',
  templateUrl: './article-form.component.html',
  styleUrls: ['./article-form.component.css'],
})
export class ArticleFormComponent implements OnInit {
  articleForm: FormGroup;
  categories: Category[];
  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((dateResult) => {
      this.categories = dateResult.data;
    });
    this.articleForm = new FormGroup({
      title: new FormControl('', [
        Validators.required,
        Validators.maxLength(50),
      ]),
      contentSummary: new FormControl('', [
        Validators.required,
        Validators.maxLength(500),
      ]),
      // contentMain: new FormControl('', Validators.required),
      categoryId: new FormControl('',Validators.required),
      languageCode: new FormControl('', Validators.required),
      // picture: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    console.log(this.articleForm.value);
  }
}
