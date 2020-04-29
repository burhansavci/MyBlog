import { Category } from './category';
import { Picture } from './picture';

export interface Article {
  id: number;
  title: string;
  contentSummary: string;
  contentMain: string;
  publishDate: Date;
  viewCount: number;
  category: Category;
  picture: Picture;
}
