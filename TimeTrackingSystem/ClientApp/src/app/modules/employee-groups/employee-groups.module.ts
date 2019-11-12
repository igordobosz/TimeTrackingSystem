import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { EmployeeGroupEditComponent } from './employee-group-edit/employee-group-edit.component';
import { EmployeeGroupIndexComponent } from './employee-group-index/employee-group-index.component';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@angular/flex-layout';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { EmployeeGroupsRoutes } from './employee-groups.routes';



@NgModule({
    declarations: [EmployeeGroupIndexComponent, EmployeeGroupEditComponent],
    imports: [
        RouterModule.forChild(EmployeeGroupsRoutes),
        CommonModule,
        CoreModule,
        SharedModule,
        FormsModule,
    ],
    entryComponents: [EmployeeGroupEditComponent],
})
export class EmployeeGroupsModule { }
