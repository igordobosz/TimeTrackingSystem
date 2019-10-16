import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterTimeEndpointDetailsComponent } from './register-time-endpoint-details.component';

describe('RegisterTimeEndpointDetailsComponent', () => {
  let component: RegisterTimeEndpointDetailsComponent;
  let fixture: ComponentFixture<RegisterTimeEndpointDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterTimeEndpointDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterTimeEndpointDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
