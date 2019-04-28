import { Injectable } from '@angular/core';
import { UserManager, User } from 'oidc-client';
import { environment } from 'src/environments/environment';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OpenIdConnectService {

  private userManager: UserManager = new UserManager(environment.openIdConnectSettings);

  private currentUser: User;

  userLoaded$ = new ReplaySubject<boolean>(1);

    //当前用户是否登录
    get userAvailable(): boolean {
      return this.currentUser != null;
    }
    //把登录用户返回
    get user(): User {
      return this.currentUser;
    }

  constructor() { 
    //清理用户管理器
    this.userManager.clearStaleState();

    //如果用户已经登录，将用户赋值给currentUser，并使userLoaded$广播true
    this.userManager.events.addUserLoaded(user => {
      if (!environment.production) {
        console.log('User loaded.', user);
      }
      this.currentUser = user;
      this.userLoaded$.next(true);
    });

    //如果用户已经登出，将null赋值给currentUser，并使userLoaded$广播false
    this.userManager.events.addUserUnloaded((e) => {
      if (!environment.production) {
        console.log('User unloaded');
      }
      this.currentUser = null;
      this.userLoaded$.next(false);
    });
  }
  
  //登录跳转
  triggerSignIn() {
    this.userManager.signinRedirect().then(() => {
      if (!environment.production) {
        console.log('Redirection to signin triggered.');
      }
    });
  }

  //登录之后跳转回调方法
  handleCallback() {
    this.userManager.signinRedirectCallback().then(user => {
      if (!environment.production) {
        console.log('Callback after signin handled.', user);
      }
    });
  }

  //刷新token跳转
  handleSilentCallback() {
    this.userManager.signinSilentCallback().then(user => {
      this.currentUser = user;
      if (!environment.production) {
        console.log('Callback after silent signin handled.', user);
      }
    });
  }

  //登出跳转
  triggerSignOut() {
    this.userManager.signoutRedirect().then(resp => {
      if (!environment.production) {
        console.log('Redirection to sign out triggered.', resp);
      }
    });
  }

}
