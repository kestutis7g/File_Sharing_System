import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TestComponent } from './pages/test/test.component';

import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { SearchComponent } from './pages/search/search.component';
import { NewGroupComponent } from './pages/newGroup/newGroup.component';
import { GroupComponent } from './pages/group/group.component';
import { AdminComponent } from './pages/admin/admin.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'search/:keyword', component: SearchComponent },
  { path: 'search/mygroups', component: SearchComponent },
  { path: 'new-group', component: NewGroupComponent },
  { path: 'group/:id', component: GroupComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'test', component: TestComponent },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
