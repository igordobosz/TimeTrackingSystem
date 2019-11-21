import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { EmployeeGroupCbxViewModel, EmployeeGroupService, EmployeeViewModel } from '../../../core/api.generated';
import { EmployeEditComponent } from '../employe-edit/employe-edit.component';

export interface FilterResult {
    cancel: boolean;
    unfilter?: boolean;
    model?: EmployeeViewModel;
}


@Component({
    selector: 'app-employe-filter',
    templateUrl: './employe-filter.component.html',
    styleUrls: ['./employe-filter.component.scss'],
})
export class EmployeFilterComponent implements OnInit {
    employee: EmployeeViewModel;
    employeeGroupsCbxList: EmployeeGroupCbxViewModel[];
    form: FormGroup;
    constructor(public dialogRef: MatDialogRef<EmployeEditComponent>,
        private employeeGroupService: EmployeeGroupService,
        private formBuilder: FormBuilder,
        @Inject(MAT_DIALOG_DATA) public model: EmployeeViewModel) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            name: [],
            surename: [],
            identityCode: [],
            employeeGroupID: [],
        });
        if (this.model != null) {
            this.form.patchValue(this.model);
        }
        this.employeeGroupService.listCbx().subscribe(e => this.employeeGroupsCbxList = e);
    }
    onNoClick(): void {
        const ans: FilterResult = { cancel: true };
        this.dialogRef.close(ans);
    }

    cancelFilters(): void {
        const ans: FilterResult = { cancel: false, unfilter: true };
        this.dialogRef.close(ans);
    }

    onSubmit(data: EmployeeViewModel) {
        const ans: FilterResult = { cancel: false, unfilter: false, model: data };
        this.dialogRef.close(ans);
    }
}
