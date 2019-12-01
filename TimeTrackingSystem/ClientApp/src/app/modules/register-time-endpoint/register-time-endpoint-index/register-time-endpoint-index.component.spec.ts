import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed, async } from '@angular/core/testing';

import { AppModule } from '../../../app.module';
import { CoreModule } from '../../../core/core.module';
import { SharedModule } from '../../../shared/shared.module';
import { RegisterTimeEndpointIndexComponent } from './register-time-endpoint-index.component';

describe('RegisterTimeEndpointIndexComponent', () => {
    let component: RegisterTimeEndpointIndexComponent;
    let fixture: ComponentFixture<RegisterTimeEndpointIndexComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [RegisterTimeEndpointIndexComponent],
            imports: [HttpClientModule, CoreModule, SharedModule],
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(RegisterTimeEndpointIndexComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});