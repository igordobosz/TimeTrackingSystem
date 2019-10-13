import { Routes } from '@angular/router';

import { RegisterTimeEndpointDetailsComponent } from './register-time-endpoint-details/register-time-endpoint-details.component';
import { RegisterTimeEndpointEditComponent } from './register-time-endpoint-edit/register-time-endpoint-edit.component';
import { RegisterTimeEndpointIndexComponent } from './register-time-endpoint-index/register-time-endpoint-index.component';

export const RegisterTimeEndpointRoutes: Routes = [
    {
        path: '',
        component: RegisterTimeEndpointIndexComponent,
        pathMatch: 'full',
    },
    {
        path: 'details/:id',
        component: RegisterTimeEndpointDetailsComponent,
        pathMatch: 'full',
    },
];
