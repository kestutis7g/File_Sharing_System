import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AvailableCultures } from './constants/culture.constants';
import { AuthService } from './services/auth.service';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
declare let require: any;

@Injectable({ providedIn: 'root' })
export class AppInitializer {
	constructor(private translateService: TranslateService, private authService: AuthService) {
		// Nothing
	}

	public async initialize(): Promise<void> {
		AvailableCultures.forEach((x) => {
			this.translateService.setTranslation(
				`${x.isoCode}`,
				// eslint-disable-next-line @typescript-eslint/no-unsafe-argument, @typescript-eslint/no-unsafe-call
				require(`../assets/i18n/${x.isoCode}.json`),
			);
		});
		const defaultCulture = AvailableCultures.find((x) => x.default === true);
		// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
		this.translateService.setDefaultLang(defaultCulture!.isoCode);

		this.authService.validateSession();
	}
}
