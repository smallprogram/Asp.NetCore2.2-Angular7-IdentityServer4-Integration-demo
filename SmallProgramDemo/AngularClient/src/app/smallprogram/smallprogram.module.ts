import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmallprogramRoutingModule } from './smallprogram-routing.module';

import { MaterialModule } from '../shared/material/material.module';

import { SmallprogramAppComponent } from './smallprogram-app.component';
import { SidenavComponent } from './component/sidenav/sidenav.component';


@NgModule({
  declarations: [SmallprogramAppComponent, SidenavComponent],
  imports: [
    CommonModule,
    MaterialModule,
    SmallprogramRoutingModule
  ]
})
export class SmallprogramModule { }
