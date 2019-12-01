import { ComponentFixture, TestBed, async } from '@angular/core/testing';

import { AuthenticationService } from '../../../core/authentication/authentication.service';
import { EmployeIndexComponent } from './employe-index.component';
import { AuthorizationService, EmployeeService, EmployeeGroupService } from '../../../core/api.generated';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

describe('EmployeIndexComponent', () => {
    let component: EmployeIndexComponent;
    let fixture: ComponentFixture<EmployeIndexComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [EmployeIndexComponent],
            providers: [AuthenticationService, AuthorizationService, { provide: MatDialogRef, useValue: {} }, { provide: MAT_DIALOG_DATA, useValue: {} }, EmployeeService, EmployeeGroupService],
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EmployeIndexComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
