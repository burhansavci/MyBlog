import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CommentService } from 'src/app/services/comment.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CaptchaService } from 'src/app/services/captcha.service';

@Component({
  selector: 'app-comment-add',
  templateUrl: './comment-add.component.html',
  styleUrls: ['./comment-add.component.css'],
})
export class CommentAddComponent implements OnInit {
  @Input() id: any;
  @Input() class: any;
  @Input() hidden: any = false;
  @Output() onAddComment = new EventEmitter<boolean>();
  commentForm: FormGroup;
  captchaSuccess: Boolean = false;
  constructor(
    public commentService: CommentService,
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private captchaService: CaptchaService
  ) {}

  ngOnInit(): void {
    this.commentForm = new FormGroup({
      name: new FormControl('', Validators.required),
      contentMain: new FormControl('', Validators.maxLength(250)),
      articleId: new FormControl(
        Number(this.route.snapshot.paramMap.get('id'))
      ),
      publishDate: new FormControl(new Date()),
      parentId: new FormControl(this.id),
      captcha: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    if (this.commentForm.valid) {
      this.commentService.addComment(this.commentForm.value).subscribe(
        (data) => {
          this.alertify.success('Your comment successfully added.');
          this.onAddComment.emit(true);
        },
        (error) => {
          this.alertify.error('An error occurred. Please try again later.');
        }
      );
    }
  }

  resolved(captchaResponse: string) {
    this.captchaService.sendToken(captchaResponse).subscribe(
      (data) => {
        this.captchaSuccess = data;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
