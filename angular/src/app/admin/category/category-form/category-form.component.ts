import { Component, OnInit, TemplateRef } from '@angular/core';
import { AlertifyService } from 'src/app/services/alertify.service';
import { LanguageService } from 'src/app/services/language.service';
import { CategoryService } from 'src/app/services/category.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Category } from 'src/app/models/category';
import { Language } from 'src/app/models/language';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css'],
})
export class CategoryFormComponent implements OnInit {
  categoryForm: FormGroup;
  category: Category;
  languages: Language[];
  isOpen: boolean = true;
  modalRef: BsModalRef;

  constructor(
    public categoryService: CategoryService,
    private languageService: LanguageService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.category = data?.dataResult?.data;
    });
    this.languageService.getLanguages().subscribe((dateResult) => {
      this.languages = dateResult.data;
    });
    this.categoryForm = new FormGroup({
      name: new FormControl(this.category?.name, Validators.required),
      description: new FormControl(
        this.category?.description,
        Validators.required
      ),
      languageCode: new FormControl(
        this.category?.languageCode,
        Validators.required
      ),
      createdDate: new FormControl(''),
      id: new FormControl(this.category?.id),
      categoryTranslationId: new FormControl(
        this.category?.categoryTranslationId
      ),
    });
  }

  onSubmit() {
    if (this.category) {
      this.categoryForm.controls.createdDate.setValue(
        this.category.createdDate
      );
      this.categoryService.updateCategory(this.categoryForm.value).subscribe(
        (result) => {
          this.alertifyService.success('Category was updated successfully');
          this.router.navigateByUrl('/admin/category/list');
        },
        (error) => {
          this.alertifyService.error(`An error occurred: ${error}`);
        }
      );
    } else {
      this.categoryForm.controls.createdDate.setValue(new Date());
      this.categoryService.addCategory(this.categoryForm.value).subscribe(
        (result) => {
          this.alertifyService.success('Category was added successfully');
          this.categoryForm.reset();
        },
        (error) => {
          this.alertifyService.error(`An error occurred: ${error}`);
          this.categoryForm.reset();
        }
      );
    }
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.categoryService.deleteCategory(this.category).subscribe(
      (result) => {
        this.alertifyService.success('Category was deleted successfully');
        this.router.navigateByUrl('/admin/category/list');
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
