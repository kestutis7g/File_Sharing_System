import { Component, OnInit } from '@angular/core';
import { Routes } from '../../constants/route.constants';
import { AuthService } from '../../services/auth.service';
import { RouteInfo } from '../../types/route.types';

@Component({
	selector: 'sidebar',
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit {
	constructor(private authService: AuthService) {
		// Nothing
	}

	public routes: RouteInfo[] = [];

	public ngOnInit(): void {
		const routes = Routes;

		this.routes = routes.map((route) => this.setIsVisible(route));
	}

	private setIsVisible(route: RouteInfo): RouteInfo {
		const isVisible = this.authService.isAuthenticated;
		route.isVisible = isVisible;

		if (route.children) {
			route.children.forEach((child) => {
				child.isVisible = isVisible;
			});
		}

		return route;
	}
}
