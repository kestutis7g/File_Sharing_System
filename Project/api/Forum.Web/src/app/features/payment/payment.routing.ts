import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OnlyAuthorizedGuard } from '../../core/guards/only-authorized.guard';
import { PaymentAuditComponent } from './payment-audit/payment-audit.component';
import { PaymentImportComponent } from './payment-import/payment-import.component';
import { PaymentListComponent } from './payment-list/payment-list.component';

const routes: Routes = [
	{
		path: 'list',
		component: PaymentListComponent,
		canActivate: [OnlyAuthorizedGuard],
	},
	{
		path: 'audit',
		component: PaymentAuditComponent,
		canActivate: [OnlyAuthorizedGuard],
	},
	{
		path: 'import',
		component: PaymentImportComponent,
		canActivate: [OnlyAuthorizedGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
	providers: [],
})
export class PaymentRouting {}
