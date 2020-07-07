import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';
import { ArticleService } from 'src/app/services/article.service';
import { Category } from 'src/app/models/category';
import { LanguageService } from 'src/app/services/language.service';
import { Language } from 'src/app/models/language';
import * as QuillNamespace from 'quill';
let Quill: any = QuillNamespace;
import ImageResize from 'quill-image-resize-module';
import { Observable } from 'rxjs';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from 'src/app/models/article';
import { Picture } from 'src/app/models/picture';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
Quill.register('modules/imageResize', ImageResize);
@Component({
  selector: 'app-article-form',
  templateUrl: './article-form.component.html',
  styleUrls: ['./article-form.component.css'],
})
export class ArticleFormComponent implements OnInit {
  articleForm: FormGroup;
  article: Article;
  categories: Category[];
  languages: Language[];
  mainPictureUrl: string;
  file: File;
  base64Objects: {
    base64String: string;
    type: string;
  }[] = [];
  isOpen: boolean = true;
  buttonMessage: string = 'Pull Up';
  modalRef: BsModalRef;
  //Quill Config
  editorConfig = {
    imageResize: true,
    syntax: true,
  };
  editorStyle = {
    height: '500px',
  };

  constructor(
    private categoryService: CategoryService,
    private languageService: LanguageService,
    public articleService: ArticleService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.article = data?.dataResult?.data;
      this.mainPictureUrl = this.article?.mainPicture.url;
    });
    this.categoryService.getCategoriesByLanguageCode().subscribe((dateResult) => {
      this.categories = dateResult.data;
    });
    this.languageService.getLanguages().subscribe((dateResult) => {
      this.languages = dateResult.data;
    });
    this.articleForm = new FormGroup({
      title: new FormControl(this.article?.title, [
        Validators.required,
        Validators.maxLength(50),
      ]),
      contentSummary: new FormControl(this.article?.contentSummary, [
        Validators.required,
        Validators.maxLength(500),
      ]),
      contentMain: new FormControl(
        this.article?.contentMain,
        Validators.required
      ),
      categoryId: new FormControl(
        this.article?.category.id,
        Validators.required
      ),
      languageCode: new FormControl(
        this.article?.languageCode,
        Validators.required
      ),
      userId: new FormControl(''),
      picture:
        this.mainPictureUrl != null
          ? new FormControl('')
          : new FormControl('', Validators.required),
      publishDate: new FormControl(''),
      id: new FormControl(this.article?.id),
      articleTranslationId: new FormControl(this.article?.articleTranslationId),
    });
    console.log(this.articleForm);
  }

  onSubmit() {
    this.articleForm.controls.categoryId.setValue(
      Number(this.articleForm.controls.categoryId.value)
    );
    this.articleForm.controls.userId.setValue(
      Number(localStorage.getItem('userId'))
    );
    if (this.article) {
      this.articleForm.controls.publishDate.setValue(this.article.publishDate);
    } else {
      this.articleForm.controls.publishDate.setValue(new Date());
    }

    const formData = new FormData();
    formData.append('title', this.articleForm.controls.title.value);
    formData.append(
      'contentSummary',
      this.articleForm.controls.contentSummary.value
    );

    this.appendPicturesToFormData(formData);
    formData.append('contentMain', this.articleForm.controls.contentMain.value);
    formData.append(
      'publishDate',
      this.article
        ? this.article.publishDate
        : this.articleForm.controls.publishDate.value.toUTCString()
    );
    formData.append('categoryId', this.articleForm.controls.categoryId.value);
    formData.append('userId', this.articleForm.controls.userId.value);
    formData.append(
      'languageCode',
      this.articleForm.controls.languageCode.value
    );
    formData.append('id', this.articleForm.controls.id.value);
    formData.append(
      'articleTranslationId',
      this.articleForm.controls.articleTranslationId.value
    );

    if (this.article) {
      this.articleService.updateArticle(formData).subscribe(
        (result) => {
          this.alertifyService.success('Article was updated successfully');
          this.router.navigateByUrl('/admin/article/list');
        },
        (error) => {
          this.alertifyService.error(`An error occurred: ${error}`);
        }
      );
    } else {
      this.articleService.addArticle(formData).subscribe(
        (result) => {
          this.alertifyService.success('Article was added successfully');
          this.articleForm.reset();
          this.mainPictureUrl = '';
        },
        (error) => {
          this.alertifyService.error(`An error occurred: ${error}`);
          this.articleForm.reset();
          this.mainPictureUrl = '';
        }
      );
    }
  }

  extractCloudinaryImagesFromContentMain(contentMain: string): Picture[] {
    const cloudinaryImgRegex: RegExp = /<img src="(http(s?):)\/\/res\.cloudinary\.com\/(?:[^\/]+\/)(?:(image)\/)?(?:(upload)\/)?\/?(?:v(\d+|\w{1,2})\/)?(?<publicId>[^\.^\s]+)/g;
    const matches: RegExpMatchArray = contentMain.match(cloudinaryImgRegex);

    const pictures: Picture[] = [];

    matches?.forEach((match) => {
      cloudinaryImgRegex.lastIndex = 0;
      const regexExpArr = cloudinaryImgRegex.exec(match);
      const picture: Picture = new Picture();
      picture.publicId = regexExpArr.groups.publicId;
      picture.isMain = false;
      pictures.push(picture);
    });

    return pictures;
  }

  extractImagesFromContentMain(contentMain: string): File[] {
    const imgRegex: RegExp = /<img src="(?<url>(data:(?<type>.+?);base64),(?<data>[^"]+))"/g;
    const matches: RegExpMatchArray = contentMain.match(imgRegex);

    this.base64Objects = [];

    matches?.forEach((match) => {
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

  appendPicturesToFormData(formData: FormData) {
    let index: number = 0;
    //append main image
    formData.append('pictures[0].isMain', 'true');
    formData.append('pictures[0].file', this.file);
    if (this.article) {
      formData.append('pictures[0].id', this.article.mainPicture.id.toString());
      formData.append('pictures[0].articleId', this.article.id.toString());
      if (!this.file) {
        formData.append(
          'pictures[0].publicId',
          this.article.mainPicture.publicId
        );
        formData.append('pictures[0].url', this.article.mainPicture.url);
      }
    }

    //append images from contentMain
    const images = this.extractImagesFromContentMain(
      this.articleForm.controls.contentMain.value
    );
    images.forEach((image, i) => {
      index++;
      formData.append(`pictures[${i + 1}].isMain`, 'false');
      formData.append(`pictures[${i + 1}].file`, image);
    });

    const existImages = this.extractCloudinaryImagesFromContentMain(
      this.articleForm.controls.contentMain.value
    );
    existImages.forEach((image) => {
      index++;
      formData.append(`pictures[${index}].isMain`, 'false');
      formData.append(`pictures[${index}].publicId`, image.publicId);
    });
  }

  toggle(event: MouseEvent) {
    event.preventDefault();
    this.isOpen = !this.isOpen;
    this.buttonMessage = this.isOpen ? 'Pull Up' : 'Pull Down';
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.articleService.deleteArticle(this.article).subscribe(
      (result) => {
        this.alertifyService.success('Article was deleted successfully');
        this.router.navigateByUrl('/admin/article/list');
        this.modalRef.hide();
      },
      (error) => {
        this.alertifyService.error(`An error occurred: ${error}`);
        this.modalRef.hide();
      }
    );
  }

  decline(): void {
    this.modalRef.hide();
  }
}
