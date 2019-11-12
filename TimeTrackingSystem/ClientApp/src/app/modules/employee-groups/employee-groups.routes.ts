import { Routes } from '@angular/router';

import { EmployeeGroupIndexComponent } from './employee-group-index/employee-group-index.component';

export const EmployeeGroupsRoutes: Routes = [
    {
        path: '',
        component: EmployeeGroupIndexComponent,
        pathMatch: 'full',
    },
];
