import { ComponentFixture, TestBed, async } from '@angular/core/testing';

import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthenticationService } from '../authentication/authentication.service';
import { HeaderComponent } from './header.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthorizationService } from '../api.generated';

describe('HeaderComponent', () => {
    let component: HeaderComponent;
    let fixture: ComponentFixture<HeaderComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [HeaderComponent],
            imports: [RouterTestingModule, HttpClientModule, SharedModule],
            providers: [AuthenticationService, AuthorizationService],
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(HeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
