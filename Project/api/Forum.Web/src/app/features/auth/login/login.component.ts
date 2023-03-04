import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../../core/services/auth.service';
import { LoginRequest } from '../../../core/types/auth.types';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
})
export class LoginComponent {
	constructor(private authService: AuthService, private translateService: TranslateService) {
		// Nothing
	}

	public error?: string;

	public form: FormGroup = new FormGroup({
		username: new FormControl<string>('', [Validators.required]),
		password: new FormControl<string>('', [Validators.required]),
	});

	public signIn(request: LoginRequest): void {
		if (!this.authService.signIn(request)) {
			this.error = this.translateService.instant('error.ERROR_INVALID_CREDENTIALS') as string;
		}
	}
}
