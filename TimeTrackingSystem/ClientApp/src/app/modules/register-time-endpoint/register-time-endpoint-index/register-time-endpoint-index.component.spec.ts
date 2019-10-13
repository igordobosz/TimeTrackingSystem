import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterTimeEndpointIndexComponent } from './register-time-endpoint-index.component';

describe('RegisterTimeEndpointIndexComponent', () => {
  let component: RegisterTimeEndpointIndexComponent;
  let fixture: ComponentFixture<RegisterTimeEndpointIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterTimeEndpointIndexComponent ]
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
