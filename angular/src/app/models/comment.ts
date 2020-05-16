export interface Comment {
  id: number;
  name: string;
  contentMain: string;
  publishDate: string;
  articleId: number;
  parentId?: number;
  subComments: Comment[];
}
