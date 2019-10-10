import { NgModule } from '@angular/core';
import { UIModule } from './ui/ui.module';
import { DeleteDialogComponent } from './components/delete-dialog/delete-dialog.component';
import { CommonModule } from '@angular/common';
import { CoreModule } from '@angular/flex-layout';

@NgModule({
  declarations: [DeleteDialogComponent],
  exports: [
    UIModule
  ],
  imports:[
    CommonModule,
    UIModule
  ],
  entryComponents: [DeleteDialogComponent]
})

export class SharedModule { }
