import { RouteInfo } from '../types/route.types';

export const Routes: RouteInfo[] = [
	{
		path: '/payment',
		title: 'menu.payments',
		type: 'sub',
		icon: 'fa-solid fa-credit-card fa-lg',
		children: [
			{
				path: '/list',
				title: 'menu.list',
				type: 'link',
			},
			{
				path: '/audit',
				title: 'menu.audit',
				type: 'link',
			},
			{
				path: '/import',
				title: 'menu.import',
				type: 'link',
			},
		],
	},
	{
		path: '/parking',
		title: 'menu.parking',
		type: 'sub',
		icon: 'fa-solid fa-square-parking fa-xl',
		children: [
			{
				path: '/list',
				title: 'menu.list',
				type: 'link',
			},
			{
				path: '/audit',
				title: 'menu.audit',
				type: 'link',
			},
		],
	},
];
