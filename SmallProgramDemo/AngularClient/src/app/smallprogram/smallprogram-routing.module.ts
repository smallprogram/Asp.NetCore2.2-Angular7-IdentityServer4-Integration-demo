import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SmallprogramAppComponent } from './smallprogram-app.component';

const routes: Routes = [
  { path: '', component: SmallprogramAppComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SmallprogramRoutingModule { }
