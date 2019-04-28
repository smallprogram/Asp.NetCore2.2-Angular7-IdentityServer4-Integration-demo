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
import { PostCardComponent } from './component/post-card/post-card.component';
import { WritePostComponent } from './component/write-post/write-post.component';
import { TinymceService } from './services/tinymce.service';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { EditorModule } from '@tinymce/tinymce-angular';




@NgModule({
  declarations: [SmallprogramAppComponent, SidenavComponent, PostListComponent, PostCardComponent, WritePostComponent],
  imports: [
    CommonModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    SmallprogramRoutingModule,
    EditorModule,
  ],
  providers: [
    PostService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizationHeaderInterceptor,
      multi: true
    },
    TinymceService
  ]
})
export class SmallprogramModule { }
