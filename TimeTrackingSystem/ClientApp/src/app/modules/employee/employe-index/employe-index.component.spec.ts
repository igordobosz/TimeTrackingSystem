import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeIndexComponent } from './employe-index.component';

describe('EmployeIndexComponent', () => {
  let component: EmployeIndexComponent;
  let fixture: ComponentFixture<EmployeIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
