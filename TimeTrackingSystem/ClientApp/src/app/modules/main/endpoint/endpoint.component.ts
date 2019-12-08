import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { RegisterTimeEndpointService, RegisterTimeResponseType } from '../../../core/api.generated';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'app-endpoint',
    templateUrl: './endpoint.component.html',
    styleUrls: ['./endpoint.component.scss'],
})
export class EndpointComponent implements OnInit {
    endpointName: string;
    securityToken: string;
    endpointID: number;
    form: FormGroup;

    constructor(private router: Router,
        public route: ActivatedRoute,
        public dialog: MatDialog,
        public location: Location,
        public snackbarService: SnackbarService,
        private endpointService: RegisterTimeEndpointService,
        private formBuilder: FormBuilder, ) { }

    ngOnInit() {
        this.endpointName = this.route.snapshot.queryParams['endpointName'];
        this.securityToken = this.route.snapshot.queryParams['securityToken'];
        if (this.endpointName == null || this.securityToken == null) {
            this.returnError();
        }
        this.endpointService.validateEndpoint(this.endpointName, this.securityToken).subscribe(r => {
            if (!r.success) {
                this.returnError();
            } else {
                this.endpointID = r.id;
            }
        });
        this.form = this.formBuilder.group({
            identityCode: ['', Validators.required],
        });
    }
    get f() { return this.form.controls; }

    async onSumbit() {
        if (this.form.invalid) {
            return;
        }
        await this.endpointService.registerTime(this.endpointID, this.f.identityCode.value).toPromise().then(r => {
            switch (r.responseType) {
                case RegisterTimeResponseType.SuccessEntrance:
                    this.snackbarService.success("Success. Enter time: " + r.entranceTime);
                    break;
                case RegisterTimeResponseType.SuccessLeave:
                    this.snackbarService.success("Success. Work time: " + r.workTime + ", Leave time: " + r.entranceTime);
                    break;
                case RegisterTimeResponseType.InWork:
                    this.snackbarService.error("You are in work now.");
                    break;
                case RegisterTimeResponseType.OutWork:
                    this.snackbarService.error("You are out of work now.");
                    break;
                case RegisterTimeResponseType.Error:
                    this.snackbarService.defaultErorr();
            }
        });
    }

    returnError() {
        this.snackbarService.error('Authorization error. Token is bad or empty.');
        this.router.navigate(['/RegisterTimeEndpoint']);
    }
}
