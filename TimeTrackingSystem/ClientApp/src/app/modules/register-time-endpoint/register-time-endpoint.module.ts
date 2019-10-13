import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterTimeEndpointIndexComponent } from './register-time-endpoint-index/register-time-endpoint-index.component';
import { RegisterTimeEndpointEditComponent } from './register-time-endpoint-edit/register-time-endpoint-edit.component';
import { RegisterTimeEndpointDetailsComponent } from './register-time-endpoint-details/register-time-endpoint-details.component';
import { RouterModule } from '@angular/router';
import { RegisterTimeEndpointRoutes } from './register-time-endpoint.routes';
import { CoreModule } from '@angular/flex-layout';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [RegisterTimeEndpointIndexComponent, RegisterTimeEndpointEditComponent, RegisterTimeEndpointDetailsComponent],
  imports: [
    RouterModule.forChild(RegisterTimeEndpointRoutes),
    CommonModule,
    CoreModule,
    SharedModule,
    FormsModule
  ]
})
export class RegisterTimeEndpointModule { }
