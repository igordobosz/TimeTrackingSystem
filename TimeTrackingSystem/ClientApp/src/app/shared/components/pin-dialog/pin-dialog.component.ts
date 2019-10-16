import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-pin-dialog',
    templateUrl: './pin-dialog.component.html',
    styleUrls: ['./pin-dialog.component.scss'],
})
export class PinDialogComponent implements OnInit {
    form: FormGroup;
    constructor(public dialogRef: MatDialogRef<PinDialogComponent>, private formBuilder: FormBuilder, ) { }
    ngOnInit() {
        this.form = this.formBuilder.group({
            pin: ['', Validators.required],
        });
    }

    onNoClick(): void {
        this.closeDialog(false);
    }

    closeDialog(result: boolean) {
        this.dialogRef.close(result);
    }

    async onSubmit(data: string) {
        if (this.form.invalid) {
            return;
        }
        const result = true;
        this.closeDialog(result);
    }
}
