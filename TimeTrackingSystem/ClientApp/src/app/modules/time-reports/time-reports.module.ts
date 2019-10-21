import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { PerEmployeeComponent } from './per-employee/per-employee.component';
import { TimeReportsRoutes } from './time-reports.routes';
import { CoreModule } from '@angular/flex-layout';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';

export const MY_FORMATS = {
    parse: {
        dateInput: 'MM/YYYY',
    },
    display: {
        dateInput: 'MM/YYYY',
        monthYearLabel: 'MMM YYYY',
        dateA11yLabel: 'LL',
        monthYearA11yLabel: 'MMMM YYYY',
    },
};



@NgModule({
    declarations: [PerEmployeeComponent],
    imports: [
        CommonModule,
        RouterModule.forChild(TimeReportsRoutes),
        CoreModule,
        SharedModule,
        FormsModule,
    ],
    providers: [
        { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
        { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
    ],
})
export class TimeReportsModule { }
