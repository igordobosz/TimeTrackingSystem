import { RegisterTimeEndpointIndexComponent } from './register-time-endpoint-index/register-time-endpoint-index.component';
import { RegisterTimeEndpointEditComponent } from './register-time-endpoint-edit/register-time-endpoint-edit.component';
import { RegisterTimeEndpointDetailsComponent } from './register-time-endpoint-details/register-time-endpoint-details.component';
import { Routes } from '@angular/router';

export const RegisterTimeEndpointRoutes: Routes = [
  {
    path: '',
    component: RegisterTimeEndpointIndexComponent,
    pathMatch: 'full'
  },
  {
    path: 'edit/:id',
    component: RegisterTimeEndpointEditComponent,
    pathMatch: 'full'
  },
  {
    path: 'insert',
    component: RegisterTimeEndpointEditComponent,
    pathMatch: 'full'
  },
  {
    path: 'details/:id',
    component: RegisterTimeEndpointDetailsComponent,
    pathMatch: 'full'
  }
];
