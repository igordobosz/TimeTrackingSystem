import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeFilterComponent } from './employe-filter.component';

describe('EmployeFilterComponent', () => {
  let component: EmployeFilterComponent;
  let fixture: ComponentFixture<EmployeFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
