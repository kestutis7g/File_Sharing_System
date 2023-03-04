import { Component, Input } from '@angular/core';

@Component({
	selector: 'parking-status',
	templateUrl: './parking-status.component.html',
	styleUrls: ['./parking-status.component.scss'],
})
export class ParkingStatusComponent {
	@Input()
	public status?: number;
}
