import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeGroupIndexComponent } from './employee-group-index.component';

describe('EmployeeGroupIndexComponent', () => {
  let component: EmployeeGroupIndexComponent;
  let fixture: ComponentFixture<EmployeeGroupIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeGroupIndexComponent ]
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
