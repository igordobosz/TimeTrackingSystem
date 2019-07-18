import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { CoreModule } from '../../core/core.module';
import { LoginComponent } from './login/login.component';



@NgModule({
  declarations: [HomeComponent, LoginComponent],
  imports: [
    CommonModule,
    CoreModule
  ]
})
export class MainModule { }
