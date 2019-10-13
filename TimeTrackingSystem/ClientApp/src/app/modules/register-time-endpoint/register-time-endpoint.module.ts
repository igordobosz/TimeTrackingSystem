import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CoreModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../../shared/shared.module';
import { RegisterTimeEndpointDetailsComponent } from './register-time-endpoint-details/register-time-endpoint-details.component';
import { RegisterTimeEndpointEditComponent } from './register-time-endpoint-edit/register-time-endpoint-edit.component';
import { RegisterTimeEndpointIndexComponent } from './register-time-endpoint-index/register-time-endpoint-index.component';
import { RegisterTimeEndpointRoutes } from './register-time-endpoint.routes';



@NgModule({
    declarations: [RegisterTimeEndpointIndexComponent, RegisterTimeEndpointEditComponent, RegisterTimeEndpointDetailsComponent],
    imports: [
        RouterModule.forChild(RegisterTimeEndpointRoutes),
        CommonModule,
        CoreModule,
        SharedModule,
        FormsModule,
    ],
    entryComponents: [RegisterTimeEndpointEditComponent],
})
export class RegisterTimeEndpointModule { }
