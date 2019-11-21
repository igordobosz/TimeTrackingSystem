import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { PageEvent } from "@angular/material/paginator";
import { Sort } from "@angular/material/sort";
import { MatTable } from "@angular/material/table";
import { Subscription } from "rxjs";
import { map, take } from 'rxjs/operators';
import {
    EmployeeGroupService,
    EmployeeService,
    EmployeeViewModel
} from "../../../core/api.generated";
import { SnackbarHelper } from "../../../core/helpers/snackbar.helper";
import { SnackbarService } from "../../../core/services/snackbar.service";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";
import { EmployeEditComponent } from '../employe-edit/employe-edit.component';
import { EmployeFilterComponent, FilterResult } from '../employe-filter/employe-filter.component';

@Component({
    selector: 'app-employe-index',
    templateUrl: './employe-index.component.html',
    styleUrls: ['./employe-index.component.scss'],
})
export class EmployeIndexComponent implements OnInit {
    @ViewChild(MatTable, { static: false }) table: MatTable<any>;
    employees: EmployeeViewModel[];
    displayedColumns: string[] = [
        'name',
        'surename',
        'identityCode',
        'employeeGroupName',
        'actions',
    ];
    sortColumn: string;
    sortOrder: string;
    pageSize = 10;
    pageIndex = 0;
    collectionSize: number;
    searchExpression: string;
    searchModel: EmployeeViewModel = new EmployeeViewModel();
    constructor(
        private employeeService: EmployeeService,
        public employeeGroupService: EmployeeGroupService,
        public dialog: MatDialog,
        private snackbarHelper: SnackbarHelper,
    ) { }

    ngOnInit() {
        this.subscribeList();
    }

    subscribeList() {
        this.employeeService
            .list(this.pageIndex, this.pageSize, this.searchExpression, this.sortColumn, this.sortOrder)
            .subscribe(e => {
                this.collectionSize = e.collectionSize;
                this.employees = e.itemList;
            });
    }

    setSort(sort: Sort): void {
        this.sortColumn = sort.active;
        this.sortOrder = sort.direction;
        this.subscribeList();
    }

    setPagination(filterEvent: PageEvent): void {
        this.pageIndex = filterEvent.pageIndex;
        this.pageSize = filterEvent.pageSize;
        this.subscribeList();
    }

    async create() {
        const dialogRef = this.dialog.open(EmployeEditComponent);

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            this.subscribeList();
        }
    }

    async edit(id: number) {
        const dialogRef = this.dialog.open(EmployeEditComponent, {
            data: id,
        });

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            this.subscribeList();
        }
    }

    async delete(id: number) {
        const emp = this.employees.find(x => x.id == id);
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: emp.name + ' ' + emp.surename,
        });

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            await this.employeeService
                .delete(id)
                .toPromise()
                .then(e => {
                    if (e.success) {
                        this.snackbarHelper.deleteSuccess();
                    } else {
                        this.snackbarHelper.deleteFail();
                    }
                });
        }
        this.subscribeList();
    }

    async filter() {
        const dialogRef = this.dialog.open(EmployeFilterComponent, { data: this.searchModel });

        const result: FilterResult = await dialogRef.afterClosed().toPromise();
        if (!result.cancel) {
            if (result.unfilter) {
                this.searchModel = new EmployeeViewModel();
                this.searchExpression = null;
            } else {
                this.searchModel = result.model;
                this.searchExpression = JSON.stringify(result.model);
            }
            this.subscribeList();
        }
    }

    getEmployeeGroupName(id?: number): string {
        let result = '';
        if (id != null) {
            this.employeeGroupService.getByID(id).subscribe(e => result = e.name);
        }
        return result;
    }
}
