/* eslint-disable max-classes-per-file */
export interface IPaginatedList<T> {
	data: T[];
	total: number;
}

export class PaginatedList<T> implements IPaginatedList<T> {
	constructor(public data: T[], public total: number) {
		// Nothing
	}
}

export type PagerSettings = {
	buttonCount: number;
	pageSizes: number[];
};

export class Pagination {
	constructor(public skip: number, public pageSize: number) {
		// Nothing
	}

	public static none(): Pagination {
		return new Pagination(0, 1000);
	}

	public static get defaultPagination(): Pagination {
		return new Pagination(0, 100);
	}

	public static pagerSettings: PagerSettings = {
		buttonCount: 5,
		pageSizes: [50, 100, 250, 1000],
	};
}
