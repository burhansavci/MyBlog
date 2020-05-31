import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';
import { ArticleService } from 'src/app/services/article.service';
import { Category } from 'src/app/models/category';
import { LanguageService } from 'src/app/services/language.service';
import { Language } from 'src/app/models/language';
import { EditorChangeContent, EditorChangeSelection } from 'ngx-quill';
import * as QuillNamespace from 'quill';
let Quill: any = QuillNamespace;
import ImageResize from 'quill-image-resize-module';
import { Observable } from 'rxjs';
import { AlertifyService } from 'src/app/services/alertify.service';
Quill.register('modules/imageResize', ImageResize);
@Component({
  selector: 'app-article-form',
  templateUrl: './article-form.component.html',
  styleUrls: ['./article-form.component.css'],
})
export class ArticleFormComponent implements OnInit {
  articleForm: FormGroup;
  categories: Category[];
  languages: Language[];
  mainPictureUrl: string;
  file: File;
  base64Objects: {
    base64String: string;
    type: string;
  }[] = [];

  //Quill Config
  editorConfig = {
    imageResize: true,
    syntax: true,
  };
  editorStyle = {
    height: '300px',
  };

  constructor(
    private categoryService: CategoryService,
    private languageService: LanguageService,
    private articleService: ArticleService,
    private alertifyService: AlertifyService
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
      contentMain: new FormControl('', Validators.required),
      categoryId: new FormControl('', Validators.required),
      languageCode: new FormControl('', Validators.required),
      userId: new FormControl(''),
      picture: new FormControl('', Validators.required),
      publishDate: new FormControl(''),
    });
  }

  onSubmit() {
    this.articleForm.controls.categoryId.setValue(
      Number(this.articleForm.controls.categoryId.value)
    );
    this.articleForm.controls.userId.setValue(
      Number(localStorage.getItem('userId'))
    );
    this.articleForm.controls.publishDate.setValue(new Date());

    console.log(this.articleForm.value);
    const formData = new FormData();
    formData.append('title', this.articleForm.controls.title.value);
    formData.append(
      'contentSummary',
      this.articleForm.controls.contentSummary.value
    );
    //append main image
    formData.append('pictures[0].isMain', 'true');
    formData.append('pictures[0].file', this.file);

    //append images from contentMain
    const images = this.extractImagesFromContentMain(
      this.articleForm.controls.contentMain.value
    );
    images.forEach((image, i) => {
      formData.append(`pictures[${i + 1}].isMain`, 'false');
      formData.append(`pictures[${i + 1}].file`, image);
    });

    formData.append('contentMain', this.articleForm.controls.contentMain.value);
    formData.append(
      'publishDate',
      this.articleForm.controls.publishDate.value.toUTCString()
    );
    formData.append('categoryId', this.articleForm.controls.categoryId.value);
    formData.append('userId', this.articleForm.controls.userId.value);
    formData.append(
      'languageCode',
      this.articleForm.controls.languageCode.value
    );

    this.articleService.addArticle(formData).subscribe(
      (result) => {
        this.alertifyService.success('Article was added successfully');
      },
      (error) => {
        this.alertifyService.error(`An error occurred: ${error}`);
      }
    );
  }

  extractImagesFromContentMain(contentMain: string): File[] {
    const imgRegex: RegExp = /<img src="(?<url>(data:(?<type>.+?);base64),(?<data>[^"]+))" width="(?<width>[0-9]+)"/g;

    const matches: RegExpMatchArray = contentMain.match(imgRegex);

    matches.forEach((match) => {
      imgRegex.lastIndex = 0;
      const regexExpArr = imgRegex.exec(match);
      this.base64Objects.push({
        base64String: regexExpArr.groups.data,
        type: regexExpArr.groups.type,
      });
    });

    const images: File[] = [];
    this.base64Objects.forEach((base64Object) => {
      const file = this.convertBase64ToFile(
        base64Object.base64String,
        base64Object.type
      );
      images.push(file);
    });

    return images;
  }

  upload(event: any) {
    this.file = event.target.files[0];
    this.convertFileToBase64(this.file).subscribe((data) => {
      this.mainPictureUrl = data;
    });
  }

  convertFileToBase64(file: File): Observable<string> {
    return Observable.create((observer) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (event: any) => {
        observer.next(event.target.result);
        observer.complete();
      };
      reader.onerror = (event: any) => {
        this.alertifyService.error(
          'File could not be read: ' + event.target.error.code
        );
        observer.next(event.target.error.code);
        observer.complete();
      };
    });
  }

  convertBase64ToFile(base64String: string, type: string): File {
    const date = new Date().valueOf();

    const imageName = `${date}.${type.split('/')[1]}'`;

    const byteString = window.atob(base64String);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const imageBlob = new Blob([int8Array], { type: type });

    return new File([imageBlob], imageName, { type: type });
  }
}
