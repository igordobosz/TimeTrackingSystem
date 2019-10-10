import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeViewModel, EmployeeService } from '../../../core/api.generated';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Subscription } from 'rxjs';
import { DeleteDialogComponent } from '../../../shared/components/delete-dialog/delete-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { SnackbarHelper } from '../../../core/helpers/snackbar.helper';
import { MatTable } from '@angular/material/table';

@Component({
  selector: 'app-employe-index',
  templateUrl: './employe-index.component.html',
  styleUrls: ['./employe-index.component.scss']
})
export class EmployeIndexComponent implements OnInit {
  @ViewChild(MatTable, {static: false}) table: MatTable<any>;
  public employees : EmployeeViewModel[];
  public displayedColumns: string[]= ['name', 'surename', 'identityCode', 'actions'];
  sortColumn: string;
  sortOrder: string;
  pageSize: number  = 10;
  pageIndex: number = 0;
  collectionSize: number;
  constructor(private employeeService: EmployeeService, public dialog: MatDialog, private snackbarHelper: SnackbarHelper) { }

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

  delete(id: number)
  {
    var emp = this.employees.find(x => x.id == id);
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: emp.name + ' ' + emp.surename
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result)
      {
        this.employeeService.delete(id).subscribe(e => {
          if (e.success)
          {
            this.snackbarHelper.deleteSuccess();
          }else{
            this.snackbarHelper.deleteFail();
          }
          this.subscribeList();
        })
      }
    });

  }
}
