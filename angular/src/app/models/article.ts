import { Category } from './category';
import { Picture } from './picture';

export interface Article {
  id: number;
  articleTranslationId: number;
  userId: number;
  title: string;
  contentSummary: string;
  contentMain: string;
  publishDate: Date;
  viewCount: number;
  languageCode: string;
  category: Category;
  pictures: Picture[];
  mainPicture: Picture;
}
