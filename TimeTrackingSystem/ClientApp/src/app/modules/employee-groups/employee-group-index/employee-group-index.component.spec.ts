import { ComponentFixture, TestBed, async } from '@angular/core/testing';

import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthorizationService, EmployeeGroupService } from '../../../core/api.generated';
import { AuthenticationService } from '../../../core/authentication/authentication.service';
import { SharedModule } from '../../../shared/shared.module';
import { EmployeeGroupIndexComponent } from './employee-group-index.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

describe('EmployeeGroupIndexComponent', () => {
    let component: EmployeeGroupIndexComponent;
    let fixture: ComponentFixture<EmployeeGroupIndexComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [EmployeeGroupIndexComponent],
            imports: [RouterTestingModule, HttpClientModule, SharedModule, BrowserAnimationsModule],
            providers: [AuthenticationService, AuthorizationService, { provide: MatDialogRef, useValue: {} }, { provide: MAT_DIALOG_DATA, useValue: {} }, EmployeeGroupService],
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EmployeeGroupIndexComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
