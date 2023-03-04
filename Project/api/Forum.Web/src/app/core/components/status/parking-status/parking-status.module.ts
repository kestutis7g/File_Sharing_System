import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { ParkingStatusComponent } from './parking-status.component';

@NgModule({
	declarations: [ParkingStatusComponent],
	imports: [CommonModule, TranslateModule],
	exports: [ParkingStatusComponent],
})
export class ParkingStatusModule {}
