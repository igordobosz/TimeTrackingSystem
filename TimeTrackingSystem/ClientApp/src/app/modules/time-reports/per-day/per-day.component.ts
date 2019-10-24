import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import moment from 'moment';

import { EmployeeService, EmployeeViewModel, RegisterTimePerDayViewModel, WorkRegisterEventService } from '../../../core/api.generated';
import { DateValidator } from '../../../shared/misc/DateValidator';

@Component({
    selector: 'app-per-day',
    templateUrl: './per-day.component.html',
    styleUrls: ['./per-day.component.scss'],
})
export class PerDayComponent implements OnInit {
    form: FormGroup;
    viewModel: RegisterTimePerDayViewModel = new RegisterTimePerDayViewModel();
    displayedColumns: string[] = [
        'employeeFullName',
        'dateGoIn',
        'dateGoOut',
        'nightWork',
        'computedTime',
        'overTime',
    ];
    date: Date;
    constructor(private employeeService: EmployeeService,
        private workRegisterEventServce: WorkRegisterEventService,
        private formBuilder: FormBuilder, ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            dpDate: [new Date(), [Validators.required]],
        });
    }
    get f() {
        return this.form.controls;
    }
    subscribeList() {
        this.workRegisterEventServce.getWorkEventsByDay(this.date).subscribe(e => {
            this.viewModel = e;
        });

    }

    async onSubmit(data: any) {
        if (this.form.invalid) {
            return;
        }
        this.date = this.f.dpDate.value;
        this.subscribeList();
    }
}
