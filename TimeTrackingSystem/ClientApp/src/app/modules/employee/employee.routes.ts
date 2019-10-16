import { Routes } from "@angular/router";
import { EmployeDetailsComponent } from './employe-details/employe-details.component';
import { EmployeEditComponent } from './employe-edit/employe-edit.component';
import { EmployeIndexComponent } from "./employe-index/employe-index.component";

export const EmployeeRoutes: Routes = [
    {
        path: '',
        component: EmployeIndexComponent,
        pathMatch: 'full',
    },
    {
        path: 'details/:id',
        component: EmployeDetailsComponent,
        pathMatch: 'full',
    },
];
