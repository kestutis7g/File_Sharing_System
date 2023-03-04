import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable, from, map } from 'rxjs';
import Swal, { SweetAlertIcon, SweetAlertOptions, SweetAlertResult } from 'sweetalert2';

@Injectable({
	providedIn: 'root',
})
export class AlertService {
	constructor(private translateService: TranslateService) {
		// Nothing
	}

	public success(title?: string, text: string = '', options?: SweetAlertOptions): void {
		if (!title) {
			title = this.translateService.instant('alert.success') as string;
		}

		Swal.fire(this.getAlertOptions(title, text, 'success', options));
	}

	public info(title?: string, text: string = '', options?: SweetAlertOptions): void {
		if (!title) {
			title = this.translateService.instant('alert.info') as string;
		}

		Swal.fire(this.getAlertOptions(title, text, 'info', options));
	}

	public error(title?: string, text: string = '', options?: SweetAlertOptions): void {
		if (!title) {
			title = this.translateService.instant('alert.error') as string;
		}

		Swal.fire(this.getAlertOptions(title, text, 'error', options));
	}

	public confirm(title?: string, text?: string, options?: SweetAlertOptions): Observable<SweetAlertResult<unknown>> {
		return from(Swal.fire(this.getConfirmOptions(title, text, options))).pipe(map((x) => x));
	}

	public confirmDelete(title?: string, text?: string, options?: SweetAlertOptions): Observable<SweetAlertResult<unknown>> {
		return from(Swal.fire(this.getConfirmDeleteOptions(title, text, options))).pipe(map((x) => x));
	}

	private getAlertOptions(title: string, text: string, icon: SweetAlertIcon, options?: SweetAlertOptions) {
		return {
			timer: 0,
			icon: icon,
			title: title,
			text: text,
			...options,
		};
	}

	private getConfirmOptions(title?: string, text?: string, options?: SweetAlertOptions): SweetAlertOptions {
		return {
			icon: 'warning',
			title: title ?? (this.translateService.instant('action.confirm') as string),
			text: text,
			showCancelButton: true,
			cancelButtonText: this.translateService.instant('action.cancel') as string,
			showConfirmButton: true,
			confirmButtonText: options?.confirmButtonText ?? (this.translateService.instant('action.confirm') as string),
			...options,
		};
	}

	private getConfirmDeleteOptions(title?: string, text?: string, options?: SweetAlertOptions): SweetAlertOptions {
		return {
			inputPlaceholder: this.translateService.instant('label.comment') as string,
			inputAttributes: {
				autocapitalize: 'off',
				autocorrect: 'off',
				maxLength: '512',
			},
			input: 'textarea',
			icon: 'warning',
			title: title ?? (this.translateService.instant('action.confirm') as string),
			text: text,
			showCancelButton: true,
			cancelButtonText: this.translateService.instant('action.cancel') as string,
			showConfirmButton: true,
			confirmButtonText: options?.confirmButtonText ?? (this.translateService.instant('action.confirm') as string),
			preConfirm: (value) => {
				if (!value) {
					Swal.showValidationMessage(this.translateService.instant('alert.message.inputRequired') as string);
				}
			},
			...options,
		};
	}
}
