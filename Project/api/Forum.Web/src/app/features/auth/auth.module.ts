import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { AuthRouting } from './auth.routing';

@NgModule({
	declarations: [LoginComponent],
	imports: [CommonModule, AuthRouting, TranslateModule, ReactiveFormsModule],
})
export class AuthModule {}
