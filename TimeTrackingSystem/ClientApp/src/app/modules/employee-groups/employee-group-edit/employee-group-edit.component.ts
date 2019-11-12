import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmployeeGroupService, EmployeeGroupViewModel } from '../../../core/api.generated';
import { SnackbarHelper } from '../../../core/helpers/snackbar.helper';

@Component({
    selector: 'app-employee-group-edit',
    templateUrl: './employee-group-edit.component.html',
    styleUrls: ['./employee-group-edit.component.scss'],
})
export class EmployeeGroupEditComponent implements OnInit {
    editMode = false;
    vm: EmployeeGroupViewModel;
    form: FormGroup;
    constructor(
        public dialogRef: MatDialogRef<EmployeeGroupEditComponent>,
        private baseService: EmployeeGroupService,
        private formBuilder: FormBuilder,
        private snackbarHelper: SnackbarHelper,
        @Inject(MAT_DIALOG_DATA) public id: number,
    ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            name: ['', Validators.required],
            workingHoursPerWeek: ['', Validators.required],
        });
        if (this.id != null) {
            this.editMode = true;
            this.baseService.getByID(this.id).subscribe(e => {
                this.vm = e;
                this.form.patchValue(this.vm);
            });
        } else {
            this.vm.init();
        }
    }

    onNoClick(): void {
        this.closeDialog(false);
    }

    closeDialog(result: boolean) {
        this.dialogRef.close(result);
    }

    async onSubmit(data: EmployeeGroupViewModel) {
        if (this.form.invalid) {
            return;
        }
        let result = false;
        if (this.editMode) {
            data.id = this.vm.id;
            await this.baseService.update(data).toPromise().then(r => {
                if (r.success) {
                    this.snackbarHelper.updateSuccess();
                    result = true;
                } else {
                    this.snackbarHelper.updateFail();
                }
            });
        } else {
            await this.baseService.insert(data).toPromise().then(
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
