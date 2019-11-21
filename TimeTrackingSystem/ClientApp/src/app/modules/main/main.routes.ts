import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../core/guards/auth.guard';
import { EndpointComponent } from './endpoint/endpoint.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';


export const MainRoutes: Routes = [
    {
        path: '',
        component: HomeComponent,
        pathMatch: 'full',
        canActivate: [AuthGuard],
    },
    {
        path: 'login',
        component: LoginComponent,
    },
    {
        path: 'endpoint',
        component: EndpointComponent,
    }
];
