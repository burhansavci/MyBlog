import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CommentService } from 'src/app/services/comment.service';
import { DataResult } from 'src/app/models/data-result';
import { Comment } from 'src/app/models/comment';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css'],
})
export class CommentListComponent implements OnInit, OnChanges {
  comments: Comment[];
  isOpen: boolean = false;
  @Input() articleId: number;
  @Input() subComments: Comment[];
  @Input() refresh: boolean = false;

  constructor(
    private commentService: CommentService,
    private alertify: AlertifyService
  ) {}

  ngOnInit(): void {
    if (this.articleId && !this.subComments) {
      this.loadComments(this.articleId);
    } else {
      this.comments = this.subComments;
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.articleId && changes.refresh && !this.subComments) {
      this.loadComments(this.articleId);
    } else {
      this.comments = this.subComments;
    }
  }

  refreshComments(changed: boolean) {
    if (this.articleId && changed && !this.subComments) {
      this.loadComments(this.articleId);
    } else {
      this.comments = this.subComments;
    }
  }

  loadComments(articleId: number) {
    this.commentService.getCommentsByArticleId(articleId).subscribe(
      (dataResult: DataResult<Comment[]>) => {
        this.comments = dataResult.data;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  handleReply(commentId: number) {
    const commentForm = document.getElementById(commentId.toString());
    const replyButton = document.getElementById(`button${commentId}`);
    if (commentForm.hasAttribute('hidden')) {
      replyButton.innerHTML = `<small class="ml-1"> <span class="btn-label"><i class="fas fa-minus-square text-primary"></i></span>Hide </small>`;
      commentForm.removeAttribute('hidden');
    } else {
      replyButton.innerHTML = `<small class="ml-1"> <span class="btn-label"><i class="fas fa-reply fa-fw text-primary"></i></span>Reply </small>`;
      commentForm.setAttribute('hidden', '');
    }
  }
}
