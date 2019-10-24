import { Routes } from '@angular/router';

import { PerDayComponent } from './per-day/per-day.component';
import { PerEmployeeComponent } from './per-employee/per-employee.component';

export const TimeReportsRoutes: Routes = [
    {
        path: 'Employee',
        component: PerEmployeeComponent,
        pathMatch: 'full',
    },
    {
        path: 'Day',
        component: PerDayComponent,
        pathMatch: 'full',
    },
];
