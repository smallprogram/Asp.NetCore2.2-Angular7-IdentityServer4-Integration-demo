import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { OpenIdConnectService } from './open-id-connect.service';


@Injectable({
  providedIn: 'root'
})
export class RequireAuthenticatedUserRouteGuard implements CanActivate {
  /**
   *
   */
  constructor(private openIdConnect:OpenIdConnectService,
    private Router:Router) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.openIdConnect.userAvailable) {
      return true;
    } else {
      //如果没登陆就跳转到登录页面
      this.openIdConnect.triggerSignIn()
      return false;
    }
    
  }

}
