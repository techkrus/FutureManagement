import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, CanActivateChild, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';


@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate, CanActivateChild {

  constructor(private authService: AuthService, private router: Router,
    private alertify: AlertifyService) { }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const roles = childRoute.data['roles'] as Array<string>;
    const url = state.url;
    if (roles) {
      const match = this.authService.roleMatch(roles);
      if (match) {
        return true;
      } else {
        if (url === '/users/details/' + this.authService.getCurrentUserId()) {
          return true;
        }
        this.router.navigate(['./no-access']);
        this.alertify.error('Du har ikke adgang til denne side');
      }
    }

    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertify.error('Adgang nægtet: Ikke logget ind.');
    this.router.navigate(['./login']);
    return false;
  }

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }
    this.alertify.error('Adgang nægtet: Ikke logget ind.');
    this.router.navigate(['./login']);
    return false;
  }
}
