import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from '../open-id-connect.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-signin-oidc',
  templateUrl: './signin-oidc.component.html',
  styleUrls: ['./signin-oidc.component.scss']
})

export class SigninOidcComponent implements OnInit {

  constructor(private openIdConnectService: OpenIdConnectService,
    private router:Router) { }

  ngOnInit() {
    //当idp登录之后跳转，接收登录用户
    this.openIdConnectService.userLoaded$.subscribe((userLoaded) => {
      //如果有用户数据，跳转至首页
      if(userLoaded)
      {
        this.router.navigate(['./']);
      }else {
        //如果没有用户数据，做日志记录
        if (!environment.production) {
          console.log('错误，用户没有登录');
        }
      }
    })
    //最有执行登录后的回调函数
    this.openIdConnectService.handleCallback();

  }

}
