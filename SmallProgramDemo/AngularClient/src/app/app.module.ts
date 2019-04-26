import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';


import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; //动画

//已经移动到shared/material模块中
//import { MatButtonModule, MatCheckboxModule } from '@angular/material'; //material按钮，复选框模块
//import { MatIconModule } from '@angular/material/icon'; //material图标模块





import { AppComponent } from './app.component';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
