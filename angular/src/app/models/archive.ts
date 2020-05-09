import { Month } from './month-archive';

export interface Archive {
	months: Month[];
	publishYear: number;
	countByYear: number;
}
