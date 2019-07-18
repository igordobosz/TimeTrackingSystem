import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { UIModule } from './ui/ui.module';

@NgModule({
  declarations: [],
  exports: [
    UIModule,
    BrowserAnimationsModule
  ]
})

export class SharedModule { }
