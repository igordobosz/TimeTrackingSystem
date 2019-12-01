import { ComponentFixture, TestBed, async } from '@angular/core/testing';

import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthorizationService, EmployeeGroupService } from '../../../core/api.generated';
import { AuthenticationService } from '../../../core/authentication/authentication.service';
import { SharedModule } from '../../../shared/shared.module';
import { EmployeeGroupEditComponent } from './employee-group-edit.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('EmployeeGroupEditComponent', () => {
    let component: EmployeeGroupEditComponent;
    let fixture: ComponentFixture<EmployeeGroupEditComponent>;


    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [EmployeeGroupEditComponent],
            imports: [RouterTestingModule, HttpClientModule, SharedModule, BrowserAnimationsModule],
            providers: [AuthenticationService, AuthorizationService, { provide: MatDialogRef, useValue: {} }, { provide: MAT_DIALOG_DATA, useValue: {} }, EmployeeGroupService],
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EmployeeGroupEditComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
