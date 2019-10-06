import { Component, OnInit } from '@angular/core';
import { EmployeeViewModel, EmployeeService } from '../../../core/api.generated';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-employe-index',
  templateUrl: './employe-index.component.html',
  styleUrls: ['./employe-index.component.scss']
})
export class EmployeIndexComponent implements OnInit {
  public employees : EmployeeViewModel[];
  public displayedColumns: string[]= ['name', 'surename', 'identityCode', 'actions'];
  sortColumn: string;
  sortOrder: string;
  pageSize: number  = 5;
  pageIndex: number = 0;
  collectionSize: number;
  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.subscribeList();
  }

  subscribeList(){
    this.employeeService.list(this.pageIndex, this.pageSize, '', this.sortColumn, this.sortOrder).subscribe(e => {
      this.collectionSize = e.collectionSize;
      this.employees = e.itemList;
    });
  }

  setSort(sort: Sort)
  {
    this.sortColumn = sort.active;
    this.sortOrder = sort.direction;
    this.subscribeList();
  }

  setPagination(filterEvent: PageEvent)
  {
    this.pageIndex = filterEvent.pageIndex;
    this.pageSize = filterEvent.pageSize;
    this.subscribeList();
  }
}
