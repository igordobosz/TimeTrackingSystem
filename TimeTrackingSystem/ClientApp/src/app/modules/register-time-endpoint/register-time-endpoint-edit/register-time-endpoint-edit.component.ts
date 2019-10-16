import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { EndpointType, RegisterTimeEndpointService, RegisterTimeEndpointViewModel } from '../../../core/api.generated';
import { SnackbarHelper } from '../../../core/helpers/snackbar.helper';
import { EmployeEditComponent } from '../../employee/employe-edit/employe-edit.component';

@Component({
    selector: 'app-register-time-endpoint-edit',
    templateUrl: './register-time-endpoint-edit.component.html',
    styleUrls: ['./register-time-endpoint-edit.component.scss'],
})
export class RegisterTimeEndpointEditComponent implements OnInit {
    editMode = false;
    viewmodel: RegisterTimeEndpointViewModel;
    form: FormGroup;
    EndpointType = EndpointType;
    constructor(
        public dialogRef: MatDialogRef<EmployeEditComponent>,
        private endpointService: RegisterTimeEndpointService,
        private formBuilder: FormBuilder,
        private snackbarHelper: SnackbarHelper,
        @Inject(MAT_DIALOG_DATA) public id: number,
    ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            name: ['', Validators.required],
            endpointType: ['', [Validators.required]],
        });
        if (this.id != null) {
            this.editMode = true;
            this.endpointService.getByID(this.id).subscribe(e => {
                this.viewmodel = e;
                this.form.patchValue(this.viewmodel);
            });
        } else {
            this.viewmodel.init();
        }
    }

    onNoClick(): void {
        this.closeDialog(false);
    }

    closeDialog(result: boolean) {
        this.dialogRef.close(result);
    }

    async onSubmit(data: RegisterTimeEndpointViewModel) {
        if (this.form.invalid) {
            return;
        }
        let result = false;
        if (this.editMode) {
            data.id = this.viewmodel.id;
            await this.endpointService.update(data).toPromise().then(r => {
                if (r.success) {
                    this.snackbarHelper.updateSuccess();
                    result = true;
                } else {
                    this.snackbarHelper.updateFail();
                }
            });
        } else {
            await this.endpointService.insert(data).toPromise().then(
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

    endpointTypeKeys(): string[] {
        const keys = Object.keys(this.EndpointType);
        return keys.slice(keys.length / 2);
    }
}
