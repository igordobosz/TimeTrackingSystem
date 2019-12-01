import { ComponentFixture, TestBed, async } from '@angular/core/testing';

import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AuthorizationService, EmployeeGroupService, EmployeeService } from '../../../core/api.generated';
import { AuthenticationService } from '../../../core/authentication/authentication.service';
import { EmployeDetailsComponent } from './employe-details.component';

describe('EmployeDetailsComponent', () => {
    let component: EmployeDetailsComponent;
    let fixture: ComponentFixture<EmployeDetailsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [EmployeDetailsComponent],
            providers: [AuthenticationService, AuthorizationService, { provide: MatDialogRef, useValue: {} }, { provide: MAT_DIALOG_DATA, useValue: {} }, EmployeeService, EmployeeGroupService],
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EmployeDetailsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
