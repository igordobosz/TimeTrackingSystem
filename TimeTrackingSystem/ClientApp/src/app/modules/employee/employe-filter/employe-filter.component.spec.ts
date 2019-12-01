import { ComponentFixture, TestBed, async } from '@angular/core/testing';

import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AuthorizationService, EmployeeGroupService, EmployeeService } from '../../../core/api.generated';
import { AuthenticationService } from '../../../core/authentication/authentication.service';
import { EmployeFilterComponent } from './employe-filter.component';

describe('EmployeFilterComponent', () => {
    let component: EmployeFilterComponent;
    let fixture: ComponentFixture<EmployeFilterComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [EmployeFilterComponent],
            providers: [AuthenticationService, AuthorizationService, { provide: MatDialogRef, useValue: {} }, { provide: MAT_DIALOG_DATA, useValue: {} }, EmployeeService, EmployeeGroupService],
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EmployeFilterComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
