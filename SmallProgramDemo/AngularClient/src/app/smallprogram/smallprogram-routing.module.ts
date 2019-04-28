import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SmallprogramAppComponent } from './smallprogram-app.component';
import { PostListComponent } from './component/post-list/post-list.component';
import { RequireAuthenticatedUserRouteGuard } from '../shared/oidc/require-authenticated-user-route.guard';

const routes: Routes = [
  {
    path: '', component: SmallprogramAppComponent, children: [
      { path: 'post-list', component: PostListComponent, canActivate: [RequireAuthenticatedUserRouteGuard] },
      { path: '**', redirectTo: 'post-list' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SmallprogramRoutingModule { }
