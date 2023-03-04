import { registerLocaleData } from '@angular/common';
import { APP_INITIALIZER, Injector, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import localeEn from '@angular/common/locales/en';
import localeLt from '@angular/common/locales/lt';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppInitializer } from './core/app.initializer';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { ServiceLocator } from './core/service-locator';
import { SidebarComponent } from './core/components/sidebar/sidebar.component';
import { SidebarRouteComponent } from './core/components/sidebar/sidebar-route/sidebar-route.component';
import { SidebarGroupComponent } from './core/components/sidebar/sidebar-group/sidebar-group.component';
import { SuccessNotificationInterceptor } from './core/interceptors/success-notification.interceptor';
import { MainLayoutComponent } from './layouts/main/main-layout.component';
import { UnauthorizedLayoutComponent } from './layouts/unauthorized/unauthorized-layout.component';

function initApp(initializer: AppInitializer) {
	return () => initializer.initialize();
}

@NgModule({
	declarations: [
		AppComponent,
		NavbarComponent,
		MainLayoutComponent,
		UnauthorizedLayoutComponent,
		SidebarComponent,
		SidebarRouteComponent,
		SidebarGroupComponent,
	],
	imports: [BrowserModule, AppRoutingModule, TranslateModule.forRoot(), HttpClientModule],
	providers: [
		AppInitializer,
		{
			provide: APP_INITIALIZER,
			useFactory: initApp,
			deps: [AppInitializer],
			multi: true,
		},
		{ provide: HTTP_INTERCEPTORS, useClass: SuccessNotificationInterceptor, multi: true },
	],
	bootstrap: [AppComponent],
})
export class AppModule {
	constructor(private injector: Injector) {
		ServiceLocator.injector = injector;
	}
}

registerLocaleData(localeEn);
registerLocaleData(localeLt);
