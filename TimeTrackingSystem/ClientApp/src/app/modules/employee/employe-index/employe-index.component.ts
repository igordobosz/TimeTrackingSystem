import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { PageEvent } from "@angular/material/paginator";
import { Sort } from "@angular/material/sort";
import { MatTable } from "@angular/material/table";
import { Subscription } from "rxjs";
import {
    EmployeeService,
    EmployeeViewModel
} from "../../../core/api.generated";
import { SnackbarHelper } from "../../../core/helpers/snackbar.helper";
import { SnackbarService } from "../../../core/services/snackbar.service";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";
import { EmployeEditComponent } from '../employe-edit/employe-edit.component';

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
        'actions'
    ];
    sortColumn: string;
    sortOrder: string;
    pageSize = 10;
    pageIndex = 0;
    collectionSize: number;
    constructor(
        private employeeService: EmployeeService,
        public dialog: MatDialog,
        private snackbarHelper: SnackbarHelper,
    ) { }

    ngOnInit() {
        this.subscribeList();
    }

    subscribeList() {
        this.employeeService
            .list(this.pageIndex, this.pageSize, '', this.sortColumn, this.sortOrder)
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
}
