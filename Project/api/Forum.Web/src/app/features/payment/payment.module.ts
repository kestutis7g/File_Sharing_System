import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { PaymentAuditComponent } from './payment-audit/payment-audit.component';
import { PaymentImportComponent } from './payment-import/payment-import.component';
import { PaymentListComponent } from './payment-list/payment-list.component';
import { PaymentRouting } from './payment.routing';

@NgModule({
	declarations: [PaymentListComponent, PaymentImportComponent, PaymentAuditComponent],
	imports: [
		CommonModule,
		PaymentRouting,
		SharedModule,
	],
})
export class PaymentModule {}
