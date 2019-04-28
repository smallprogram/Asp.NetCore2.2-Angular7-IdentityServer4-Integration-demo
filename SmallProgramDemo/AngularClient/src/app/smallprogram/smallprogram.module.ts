import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmallprogramRoutingModule } from './smallprogram-routing.module';

import { MaterialModule } from '../shared/material/material.module';

import { SmallprogramAppComponent } from './smallprogram-app.component';
import { SidenavComponent } from './component/sidenav/sidenav.component';
import { PostService } from './services/post.service';
import { PostListComponent } from './component/post-list/post-list.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizationHeaderInterceptor } from '../shared/oidc/authorization-header-interceptor.interceptor';


@NgModule({
  declarations: [SmallprogramAppComponent, SidenavComponent, PostListComponent],
  imports: [
    CommonModule,
    MaterialModule,
    HttpClientModule,
    SmallprogramRoutingModule
  ],
  providers: [
    PostService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizationHeaderInterceptor,
      multi: true
    }
  ]
})
export class SmallprogramModule { }
