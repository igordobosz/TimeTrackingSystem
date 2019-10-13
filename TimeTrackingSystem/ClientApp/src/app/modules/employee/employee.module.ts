import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../../core/core.module';
import { SharedModule } from '../../shared/shared.module';
import { EmployeDetailsComponent } from './employe-details/employe-details.component';
import { EmployeEditComponent } from './employe-edit/employe-edit.component';
import { EmployeIndexComponent } from './employe-index/employe-index.component';
import { EmployeeRoutes } from './employee.routes';



@NgModule({
    declarations: [EmployeIndexComponent, EmployeDetailsComponent, EmployeEditComponent],
    imports: [
        RouterModule.forChild(EmployeeRoutes),
        CommonModule,
        CoreModule,
        SharedModule,
        FormsModule,
    ],
    entryComponents: [EmployeEditComponent],
})
export class EmployeeModule { }
