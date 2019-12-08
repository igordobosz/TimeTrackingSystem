import { Injectable } from '@angular/core';

import { SnackbarService } from '../services/snackbar.service';

@Injectable({
    providedIn: 'root',
})
export class SnackbarHelper {

    constructor(private snackbarService: SnackbarService) { }

    deleteSuccess() {
        this.snackbarService.success('Successfully deleted');
    }

    deleteFail() {
        this.snackbarService.error('Deleting failed');
    }

    insertSuccess() {
        this.snackbarService.success('Successfully inserted');
    }

    insertFail() {
        this.snackbarService.success('Inserting failed');
    }

    updateSuccess() {
        this.snackbarService.success('Successfully edited.');
    }

    updateFail() {
        this.snackbarService.success('Editing failed.');
    }
}
