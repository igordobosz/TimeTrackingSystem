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

        this.f.identityCode.setValue('a');
    }
    get f() { return this.form.controls; }

    async onSumbit() {
        if (this.form.invalid) {
            return;
        }
        console.log(this.endpointID);
        await this.endpointService.registerTime(this.endpointID, this.f.identityCode.value).toPromise().then(r => {
            switch (r.responseType) {
                case RegisterTimeResponseType.Success:
                    this.snackbarService.error("Udało się zarejestrować. Czas wejścia: " + r.workTime);
                    break;
                case RegisterTimeResponseType.InWork:
                    this.snackbarService.error("Jestes aktualnie w pracy.");
                    break;
                case RegisterTimeResponseType.OutWork:
                    this.snackbarService.error("Jestes aktualnie poza praca.");
                    break;
                case RegisterTimeResponseType.Error:
                    this.snackbarService.defaultErorr();
            }
        });
    }

    returnError() {
        this.snackbarService.error('Błąd autoryzacji. Brak tokena lub niepoprawny.');
        this.router.navigate(['/RegisterTimeEndpoint']);
    }
}
