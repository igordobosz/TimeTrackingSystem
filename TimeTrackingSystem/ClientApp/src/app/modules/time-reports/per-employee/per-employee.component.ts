import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { PageEvent } from '@angular/material/paginator';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { Moment, default as _rollupMoment } from 'moment';
import { Observable, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { EmployeeService, EmployeeViewModel, RegisterTimePerEmployeeDayWrapperViewModel, RegisterTimePerEmployeeViewModel, WorkRegisterEventService } from '../../../core/api.generated';
import { SnackbarHelper } from '../../../core/helpers/snackbar.helper';
import { DateValidator } from '../../../shared/misc/DateValidator';

const moment = _rollupMoment || _moment;

@Component({
    selector: 'app-per-employee',
    templateUrl: './per-employee.component.html',
    styleUrls: ['./per-employee.component.scss'],
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
    constructor(private employeeService: EmployeeService,
        private workRegisterEventServce: WorkRegisterEventService,
        private formBuilder: FormBuilder,
        private snackbarHelper: SnackbarHelper, ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            slcEmployee: ['', Validators.required],
            dpDate: new FormControl(moment(), [Validators.required, DateValidator.dateVaidator]),
        });
        this.f.slcEmployee.valueChanges.subscribe(val => this.filterEmployees = this.employeeService.filterEmployeeAutoComplete(val));
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
        this.workRegisterEventServce.getWorkEventsByEmployeeAndDate(this.employee.id, this.date).subscribe(e => {
            this.viewModel = e;
        });

    }

    async onSubmit(data: any) {
        if (this.form.invalid) {
            return;
        }
        this.employee = this.f.slcEmployee.value as EmployeeViewModel;
        this.date = this.f.dpDate.value.toDate();
        this.subscribeList();
    }

}
