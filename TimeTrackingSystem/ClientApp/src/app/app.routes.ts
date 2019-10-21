import { Routes } from "@angular/router";
import { AuthGuard } from './core/guards/auth.guard';

export const AppRootRoutes: Routes = [
    {
        path: '',
        loadChildren: './modules/main/main.module#MainModule',
    },
    {
        path: 'Employee',
        canActivate: [AuthGuard],
        loadChildren: './modules/employee/employee.module#EmployeeModule',
    },
    {
        path: 'RegisterTimeEndpoint',
        canActivate: [AuthGuard],
        loadChildren: './modules/register-time-endpoint/register-time-endpoint.module#RegisterTimeEndpointModule',
    },
    {
        path: 'Reports',
        canActivate: [AuthGuard],
        loadChildren: './modules/time-reports/time-reports.module#TimeReportsModule',
    }
];
