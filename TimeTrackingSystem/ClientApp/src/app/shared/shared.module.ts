import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CoreModule } from '@angular/flex-layout';
import { DeleteDialogComponent } from './components/delete-dialog/delete-dialog.component';
import { PinDialogComponent } from './components/pin-dialog/pin-dialog.component';
import { UIModule } from './ui/ui.module';

@NgModule({
    declarations: [DeleteDialogComponent, PinDialogComponent],
    exports: [
        UIModule,
    ],
    imports: [
        CommonModule,
        UIModule,
    ],
    entryComponents: [DeleteDialogComponent, PinDialogComponent],
})

export class SharedModule { }
