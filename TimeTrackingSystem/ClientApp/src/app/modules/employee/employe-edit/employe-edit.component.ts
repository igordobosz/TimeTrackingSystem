import { Location } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import {
    EmployeeService,
    EmployeeViewModel,
    IEmployeeViewModel
} from "../../../core/api.generated";
import { SnackbarHelper } from '../../../core/helpers/snackbar.helper';

@Component({
    selector: 'app-employe-edit',
    templateUrl: './employe-edit.component.html',
    styleUrls: ['./employe-edit.component.scss'],
})
export class EmployeEditComponent implements OnInit {
    editMode = false;
    employee: EmployeeViewModel;
    form: FormGroup;
    constructor(
        public dialogRef: MatDialogRef<EmployeEditComponent>,
        private route: ActivatedRoute,
        private employeeService: EmployeeService,
        private location: Location,
        private formBuilder: FormBuilder,
        private snackbarHelper: SnackbarHelper,
        @Inject(MAT_DIALOG_DATA) public id: number,
    ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            name: ['', Validators.required],
            surename: ['', Validators.required],
            identityCode: ['', Validators.required],
        });
        if (this.id != null) {
            this.editMode = true;
            this.employeeService.getByID(this.id).subscribe(e => {
                this.employee = e;
                this.form.patchValue(this.employee);
            });
        } else {
            this.employee.init();
        }
    }

    onNoClick(): void {
        this.closeDialog(false);
    }

    closeDialog(result: boolean) {
        this.dialogRef.close(result);
    }

    async onSubmit(data: EmployeeViewModel) {
        if (this.form.invalid) {
            return;
        }
        let result = false;
        if (this.editMode) {
            data.id = this.employee.id;
            await this.employeeService.update(data).toPromise().then(r => {
                if (r.success) {
                    this.snackbarHelper.updateSuccess();
                    result = true;
                } else {
                    this.snackbarHelper.updateFail();
                }
            });
        } else {
            await this.employeeService.insert(data).toPromise().then(
                r => {
                    if (r.success) {
                        this.snackbarHelper.insertSuccess();
                        result = true;
                    } else {
                        this.snackbarHelper.insertFail();
                    }
                },
            );
        }
        this.closeDialog(result);
    }
}
