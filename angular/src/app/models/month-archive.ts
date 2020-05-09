import { ArticleArchive } from './article-archive';

export interface Month {
	articleArchives: ArticleArchive[];
	publishMonth: number;
	monthName: string;
	countByMonth: number;
}
