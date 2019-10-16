import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterTimeEndpointEditComponent } from './register-time-endpoint-edit.component';

describe('RegisterTimeEndpointEditComponent', () => {
  let component: RegisterTimeEndpointEditComponent;
  let fixture: ComponentFixture<RegisterTimeEndpointEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterTimeEndpointEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterTimeEndpointEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
