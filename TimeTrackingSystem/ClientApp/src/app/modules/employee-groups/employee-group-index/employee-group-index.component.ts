import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';

import { EmployeeGroupService, EmployeeGroupViewModel } from '../../../core/api.generated';
import { SnackbarHelper } from '../../../core/helpers/snackbar.helper';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { DeleteDialogComponent } from '../../../shared/components/delete-dialog/delete-dialog.component';
import { EmployeeGroupEditComponent } from '../employee-group-edit/employee-group-edit.component';

@Component({
    selector: 'app-employee-group-index',
    templateUrl: './employee-group-index.component.html',
    styleUrls: ['./employee-group-index.component.scss'],
})
export class EmployeeGroupIndexComponent implements OnInit {

    @ViewChild(MatTable, { static: false }) table: MatTable<any>;
    dataSource: EmployeeGroupViewModel[];
    displayedColumns: string[] = [
        'name',
        'workingHoursPerWeek',
        'actions',
    ];
    sortColumn: string;
    sortOrder: string;
    pageSize = 10;
    pageIndex = 0;
    collectionSize: number;
    constructor(
        private baseService: EmployeeGroupService,
        public dialog: MatDialog,
        private snackbarHelper: SnackbarHelper,
        private snackbarService: SnackbarService,
        private router: Router,
    ) { }

    ngOnInit() {
        this.subscribeList();
    }

    subscribeList() {
        this.baseService
            .list(this.pageIndex, this.pageSize, '', this.sortColumn, this.sortOrder)
            .subscribe(e => {
                this.collectionSize = e.collectionSize;
                this.dataSource = e.itemList;
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
        const dialogRef = this.dialog.open(EmployeeGroupEditComponent);

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            this.subscribeList();
        }
    }

    async edit(id: number) {
        const dialogRef = this.dialog.open(EmployeeGroupEditComponent, {
            data: id,
        });

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            this.subscribeList();
        }
    }

    async delete(id: number) {
        const vm = this.dataSource.find(x => x.id === id);
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: vm.name,
        });

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            await this.baseService
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
