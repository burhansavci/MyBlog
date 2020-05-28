import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';
import { LanguageService } from 'src/app/services/language.service';
import { Language } from 'src/app/models/language';

@Component({
  selector: 'app-article-form',
  templateUrl: './article-form.component.html',
  styleUrls: ['./article-form.component.css'],
})
export class ArticleFormComponent implements OnInit {
  articleForm: FormGroup;
  categories: Category[];
  languages: Language[];
  constructor(
    private categoryService: CategoryService,
    private languageService: LanguageService
  ) {}

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((dateResult) => {
      this.categories = dateResult.data;
    });
    this.languageService.getLanguages().subscribe((dateResult) => {
      this.languages = dateResult.data;
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
      categoryId: new FormControl('', Validators.required),
      languageCode: new FormControl('', Validators.required),
      // picture: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    console.log(this.articleForm.value);
  }
}
