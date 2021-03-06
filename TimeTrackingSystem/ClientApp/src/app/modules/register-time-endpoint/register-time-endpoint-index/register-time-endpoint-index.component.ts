import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';

import { EndpointType, RegisterTimeEndpointService, RegisterTimeEndpointViewModel } from '../../../core/api.generated';
import { SnackbarHelper } from '../../../core/helpers/snackbar.helper';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { DeleteDialogComponent } from '../../../shared/components/delete-dialog/delete-dialog.component';
import { RegisterTimeEndpointEditComponent } from '../register-time-endpoint-edit/register-time-endpoint-edit.component';

@Component({
    selector: 'app-register-time-endpoint-index',
    templateUrl: './register-time-endpoint-index.component.html',
    styleUrls: ['./register-time-endpoint-index.component.scss'],
})
export class RegisterTimeEndpointIndexComponent implements OnInit {

    @ViewChild(MatTable, { static: false }) table: MatTable<any>;
    dataSource: RegisterTimeEndpointViewModel[];
    displayedColumns: string[] = [
        'name',
        'endpointType',
        'securityToken',
        'actions',
    ];
    sortColumn: string;
    sortOrder: string;
    pageSize = 10;
    pageIndex = 0;
    collectionSize: number;
    EndpointType = EndpointType;
    constructor(
        private endpointService: RegisterTimeEndpointService,
        public dialog: MatDialog,
        private snackbarHelper: SnackbarHelper,
        private snackbarService: SnackbarService,
        private router: Router,
    ) { }

    ngOnInit() {
        this.subscribeList();
    }

    subscribeList() {
        this.endpointService
            .list(this.pageIndex, this.pageSize, null, this.sortColumn, this.sortOrder)
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
        const dialogRef = this.dialog.open(RegisterTimeEndpointEditComponent);

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            this.subscribeList();
        }
    }

    async edit(id: number) {
        const dialogRef = this.dialog.open(RegisterTimeEndpointEditComponent, {
            data: id,
        });

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            this.subscribeList();
        }
    }

    async delete(id: number) {
        const vm = this.dataSource.find(x => x.id == id);
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: vm.name,
        });

        const result = await dialogRef.afterClosed().toPromise();
        if (result) {
            await this.endpointService
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

    async generateToken(id: number) {
        const result = await this.endpointService.generateToken(id).toPromise().then(res => {
            if (res.success) {
                this.snackbarService.success('Pomyślnie wygenerowano token.');
            } else {
                this.snackbarService.success('Nie udało się wygenerować tokenu.');
            }
        },
        );
        this.subscribeList();
    }

    goToEndpoint(id: number) {
        const vm = this.dataSource.find(x => x.id == id);
        this.router.navigate(['/endpoint'], { queryParams: { endpointName: vm.name, securityToken: vm.securityToken } });
    }

}
