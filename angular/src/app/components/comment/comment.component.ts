import { Component, OnInit, Input } from '@angular/core';
@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
})
export class CommentComponent implements OnInit {
  @Input() articleId: number;
  changed: boolean;
  constructor() {}

  ngOnInit(): void {}

  refreshCommentList(changed: boolean) {
    this.changed = changed;
  }
}
