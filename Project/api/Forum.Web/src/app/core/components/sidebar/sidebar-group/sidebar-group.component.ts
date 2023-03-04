import { Component, Input } from '@angular/core';

@Component({
	selector: 'sidebar-group',
	templateUrl: './sidebar-group.component.html',
	styleUrls: ['./sidebar-group.component.scss'],
})
export class SidebarGroupComponent {
	@Input()
	public icon?: string;

	@Input()
	public title?: string;

	@Input()
	public route?: string;
}
