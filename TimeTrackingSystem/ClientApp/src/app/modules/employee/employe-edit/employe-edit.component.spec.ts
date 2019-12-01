import { ComponentFixture, TestBed, async } from '@angular/core/testing';

import { HttpClientModule } from '@angular/common/http';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthorizationService, EmployeeService, EmployeeGroupService } from '../../../core/api.generated';
import { AuthenticationService } from '../../../core/authentication/authentication.service';
import { SharedModule } from '../../../shared/shared.module';
import { EmployeEditComponent } from './employe-edit.component';

describe('EmployeEditComponent', () => {
    let component: EmployeEditComponent;
    let fixture: ComponentFixture<EmployeEditComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [EmployeEditComponent],
            imports: [RouterTestingModule, HttpClientModule, SharedModule, BrowserAnimationsModule],
            providers: [AuthenticationService, AuthorizationService, { provide: MatDialogRef, useValue: {} }, { provide: MAT_DIALOG_DATA, useValue: {} }, EmployeeService, EmployeeGroupService],
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EmployeEditComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
