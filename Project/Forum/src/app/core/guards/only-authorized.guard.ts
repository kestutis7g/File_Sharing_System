import { Injectable } from '@angular/core';
import {
  CanActivate,
  CanLoad,
  Router,
  Route,
  UrlSegment,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';

@Injectable({ providedIn: 'root' })
export class OnlyAuthorizedGuard implements CanActivate, CanLoad {
  constructor(private router: Router) {
    // Nothing
  }

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  public canLoad(route: Route, segments: UrlSegment[]): boolean {
    return this.canAccess();
  }

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  public canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    return this.canAccess();
  }

  public canAccess(): boolean {
    if (localStorage.getItem('token')) {
      return true;
    }
    this.router.navigate(['/login']);
    return false;
  }
}
