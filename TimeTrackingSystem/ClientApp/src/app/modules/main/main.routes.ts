import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from '../../core/guards/auth.guard';
import { LoginComponent } from './login/login.component';


export const MainRoutes: Routes = [
  {
      path: '',
      component: HomeComponent,
      pathMatch: 'full',
      canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  }
];
