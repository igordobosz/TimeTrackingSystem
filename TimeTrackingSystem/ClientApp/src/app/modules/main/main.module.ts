import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { CoreModule } from '../../core/core.module';
import { LoginComponent } from './login/login.component';
import { SharedModule } from '../../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MainRoutes } from './main.routes';



@NgModule({
  declarations: [HomeComponent, LoginComponent],
  imports: [
    RouterModule.forChild(MainRoutes),
    CommonModule,
    CoreModule,
    SharedModule
  ]
})
export class MainModule { }
