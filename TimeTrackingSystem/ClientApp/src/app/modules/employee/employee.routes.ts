import { Routes } from "@angular/router";
import { EmployeIndexComponent } from "./employe-index/employe-index.component";
import { EmployeEditComponent } from './employe-edit/employe-edit.component';
import { EmployeDetailsComponent } from './employe-details/employe-details.component';

export const EmployeeRoutes: Routes = [
  {
    path: '',
    component: EmployeIndexComponent,
    pathMatch: 'full'
  },
  {
    path: 'edit',
    component: EmployeEditComponent,
    pathMatch: 'full'
  },
  {
    path: 'details',
    component: EmployeDetailsComponent,
    pathMatch: 'full'
  }
];
