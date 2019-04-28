import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninOidcComponent } from './shared/oidc/signin-oidc/signin-oidc.component';
import { RedirectSilentRenewComponent } from './shared/oidc/redirect-silent-renew/redirect-silent-renew.component';

const routes: Routes = [
  { path: 'signin-oidc', component: SigninOidcComponent },
  { path: 'redirect-silentrenew', component: RedirectSilentRenewComponent },
  { path: 'smallprogram', loadChildren: './smallprogram/smallprogram.module#SmallprogramModule' },
  { path: '**', redirectTo: 'smallprogram' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
