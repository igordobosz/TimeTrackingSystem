import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeGroupEditComponent } from './employee-group-edit.component';

describe('EmployeeGroupEditComponent', () => {
  let component: EmployeeGroupEditComponent;
  let fixture: ComponentFixture<EmployeeGroupEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeGroupEditComponent ]
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
