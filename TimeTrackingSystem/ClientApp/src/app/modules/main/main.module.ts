import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { CoreModule } from '../../core/core.module';
import { LoginComponent } from './login/login.component';
import { SharedModule } from '../../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [HomeComponent, LoginComponent],
  imports: [
    CommonModule,
    CoreModule,
    SharedModule
  ]
})
export class MainModule { }
