import { CdkColumnDef } from '@angular/cdk/table';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { Moment, default as _rollupMoment } from 'moment';
import { Observable, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { EmployeeService, EmployeeViewModel, RegisterTimePerEmployeeDayWrapperViewModel, RegisterTimePerEmployeeViewModel, WorkRegisterEventService } from '../../../core/api.generated';
import { SnackbarHelper } from '../../../core/helpers/snackbar.helper';
import { DateValidator } from '../../../shared/misc/DateValidator';

const moment = _rollupMoment || _moment;

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
@Component({
    selector: 'app-per-employee',
    templateUrl: './per-employee.component.html',
    styleUrls: ['./per-employee.component.scss'],
    providers: [{ provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }, { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },],
})

export class PerEmployeeComponent implements OnInit {
    form: FormGroup;
    filterEmployees: Observable<EmployeeViewModel[]>;
    viewModel: RegisterTimePerEmployeeViewModel = new RegisterTimePerEmployeeViewModel();
    displayedColumns: string[] = [
        'day',
        'dateGoIn',
        'dateGoOut',
        'nightWork',
        'computedTime',
        'overTime',
    ];
    collectionSize: number;

    employee: EmployeeViewModel;
    date: Date;
    chkAttended: boolean;
    tolerance: number;
    sumOvertimes: boolean;
    constructor(private employeeService: EmployeeService,
        private workRegisterEventServce: WorkRegisterEventService,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute, ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            slcEmployee: ['', Validators.required],
            dpDate: new FormControl(moment(), [Validators.required, DateValidator.dateVaidator]),
            tolerance: [10, [Validators.required, Validators.max(29)]],
            sumOvertimes: [true, []],
        });
        this.f.slcEmployee.valueChanges.subscribe(val => this.filterEmployees = this.employeeService.filterEmployeeAutoComplete(val));
        this.route.queryParams.subscribe(params => {
            const employeeID = params['employeeID'];
            if (employeeID != null) {
                this.employeeService.getByID(employeeID).subscribe(emp => {
                    if (emp != null) {
                        this.employee = emp;
                        this.date = new Date();
                        this.f.slcEmployee.setValue(emp);
                        this.subscribeList();
                    }
                });
            }
        });
    }

    get f() {
        return this.form.controls;
    }

    chosenYearHandler(normalizedYear: Moment) {
        const ctrlValue = this.f.dpDate.value;
        ctrlValue.year(normalizedYear.year());
        this.f.dpDate.setValue(ctrlValue);
    }

    chosenMonthHandler(normalizedMonth: Moment, datepicker: MatDatepicker<Moment>) {
        const ctrlValue = this.f.dpDate.value;
        ctrlValue.month(normalizedMonth.month());
        this.f.dpDate.setValue(ctrlValue);
        datepicker.close();
    }

    displayFn(emp?: EmployeeViewModel): string | undefined {
        return emp ? emp.name + ' ' + emp.surename : undefined;
    }


    subscribeList() {
        this.workRegisterEventServce.getWorkEventsByEmployeeAndDate(this.employee.id, this.date, this.sumOvertimes, this.tolerance).subscribe(e => {
            this.viewModel = e;
        });

    }

    async onSubmit(data: any) {
        if (this.form.invalid) {
            return;
        }
        this.employee = this.f.slcEmployee.value as EmployeeViewModel;
        this.date = this.f.dpDate.value.toDate();
        this.sumOvertimes = this.f.sumOvertimes.value;
        this.tolerance = this.f.tolerance.value;
        this.subscribeList();
    }
}
