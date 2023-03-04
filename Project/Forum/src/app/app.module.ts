import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { ThemeModule } from './theme/theme.module';
import { lightTheme } from './theme/light-theme';
import { darkTheme } from './theme/dark-theme';

import { TestComponent } from './pages/test/test.component';

import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { SearchComponent } from './pages/search/search.component';

import { NewGroupComponent } from './pages/newGroup/newGroup.component';
import { GroupComponent } from './pages/group/group.component';

import { NewPostComponent } from './core/components/newPost/newPost.component';

import { AdminComponent } from './pages/admin/admin.component';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import {
  UtilsDropdownsModule,
  UtilsGridModule,
  UtilsInputsModule,
  UtilsLayoutModule,
  UtilsLoaderModule,
} from 'angular-helper-utils';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    SearchComponent,
    NewGroupComponent,
    GroupComponent,
    NewPostComponent,
    AdminComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ThemeModule.forRoot({
      themes: [lightTheme, darkTheme],
      active: 'light',
    }),
    CollapseModule.forRoot(),
    UtilsGridModule,
    UtilsInputsModule,
    UtilsDropdownsModule,
    UtilsLayoutModule,
    UtilsLoaderModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
