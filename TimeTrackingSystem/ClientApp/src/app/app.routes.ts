import { Routes } from "@angular/router";
import { AuthGuard } from './core/guards/auth.guard';

export const AppRootRoutes: Routes = [
  {
    path: '',
    loadChildren: './modules/main/main.module#MainModule'
  },
  {
    path: 'employee',
    canActivate: [AuthGuard],
    loadChildren: './modules/employee/employee.module#EmployeeModule'
  }
];
