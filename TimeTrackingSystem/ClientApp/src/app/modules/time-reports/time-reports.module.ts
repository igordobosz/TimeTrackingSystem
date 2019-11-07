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
import { PerDayComponent } from './per-day/per-day.component';

@NgModule({
    declarations: [PerEmployeeComponent, PerDayComponent],
    imports: [
        CommonModule,
        RouterModule.forChild(TimeReportsRoutes),
        CoreModule,
        SharedModule,
        FormsModule,
    ],
    providers: [

    ],
})
export class TimeReportsModule { }
