import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Language } from 'src/app/models/language';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LanguageService } from 'src/app/services/language.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-language-form',
  templateUrl: './language-form.component.html',
  styleUrls: ['./language-form.component.css'],
})
export class LanguageFormComponent implements OnInit {
  languageForm: FormGroup;
  language: Language;
  modalRef: BsModalRef;

  constructor(
    public languageService: LanguageService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.language = data?.dataResult?.data;
    });
    this.languageForm = new FormGroup({
      name: new FormControl(this.language?.name, Validators.required),
      languageCode: new FormControl(
        this.language?.languageCode,
        Validators.required
      ),
      isDefault: new FormControl(this.language?.isDefault ? true : false),
    });
  }

  onSubmit() {
    if (this.language) {
      this.languageService.updateLanguage(this.languageForm.value).subscribe(
        (result) => {
          this.alertifyService.success('Language was updated successfully');
          this.router.navigateByUrl('/admin/language/list');
        },
        (error) => {
          this.alertifyService.error(`An error occurred: ${error}`);
        }
      );
    } else {
      this.languageService.addLanguage(this.languageForm.value).subscribe(
        (result) => {
          this.alertifyService.success('Language was added successfully');
          this.languageForm.reset();
        },
        (error) => {
          this.alertifyService.error(`An error occurred: ${error}`);
          this.languageForm.reset();
        }
      );
    }
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.languageService.deleteLanguage(this.language).subscribe(
      (result) => {
        this.alertifyService.success('Language was deleted successfully');
        this.router.navigateByUrl('/admin/language/list');
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
