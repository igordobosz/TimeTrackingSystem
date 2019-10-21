import { Routes } from '@angular/router';

import { PerEmployeeComponent } from './per-employee/per-employee.component';

export const TimeReportsRoutes: Routes = [
    {
        path: 'Employee',
        component: PerEmployeeComponent,
        pathMatch: 'full',
    },
];
