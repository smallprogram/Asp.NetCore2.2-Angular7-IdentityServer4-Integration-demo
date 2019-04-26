import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: 'smallprogram', loadChildren: './smallprogram/smallprogram.module#SmallprogramModule' },
  { path: '**', redirectTo: 'smallprogram' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
