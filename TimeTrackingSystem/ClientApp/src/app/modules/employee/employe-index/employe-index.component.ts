import { Component, OnInit } from '@angular/core';
import { EmployeeViewModel, EmployeeService } from '../../../core/api.generated';

@Component({
  selector: 'app-employe-index',
  templateUrl: './employe-index.component.html',
  styleUrls: ['./employe-index.component.scss']
})
export class EmployeIndexComponent implements OnInit {
  public employees : EmployeeViewModel[];
  public displayedColumns: string[] = ['name', 'surename', 'identity-code'];
  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.employeeService.list(0,0,0,0,'','','').subscribe(e => this.employees = e);
  }

}
