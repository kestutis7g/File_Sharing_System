declare type RouteType = 'sub' | 'link';

export type BaseRoute = {
	path: string;
	title: string;
	type: RouteType;
	isVisible?: boolean;
};

export type RouteInfo = BaseRoute & {
	icon: string;
	children?: BaseRoute[];
};
