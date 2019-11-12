import { CdkColumnDef } from '@angular/cdk/table';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CoreModule } from '@angular/flex-layout';

import { DeleteDialogComponent } from './components/delete-dialog/delete-dialog.component';
import { PinDialogComponent } from './components/pin-dialog/pin-dialog.component';
import { FilterWorkEventsAttended } from './pipes/FilterWorkEventsAttended.pipe';
import { UIModule } from './ui/ui.module';

@NgModule({
    declarations: [DeleteDialogComponent, PinDialogComponent, FilterWorkEventsAttended],
    exports: [
        UIModule,
        FilterWorkEventsAttended,
    ],
    imports: [
        CommonModule,
        UIModule,
    ],
    entryComponents: [DeleteDialogComponent, PinDialogComponent],
    providers: [CdkColumnDef],
})

export class SharedModule { }
