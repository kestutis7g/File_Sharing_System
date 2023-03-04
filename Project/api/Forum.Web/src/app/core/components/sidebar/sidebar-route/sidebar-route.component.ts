import { Component, Input } from '@angular/core';

@Component({
	selector: 'sidebar-route',
	templateUrl: './sidebar-route.component.html',
	styleUrls: ['./sidebar-route.component.scss'],
})
export class SidebarRouteComponent {
	@Input()
	public title?: string;

	@Input()
	public route?: string;
}
