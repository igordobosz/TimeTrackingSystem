import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeIndexComponent } from './employe-index/employe-index.component';
import { EmployeDetailsComponent } from './employe-details/employe-details.component';
import { EmployeEditComponent } from './employe-edit/employe-edit.component';
import { CoreModule } from '../../core/core.module';
import { SharedModule } from '../../shared/shared.module';
import { RouterModule } from '@angular/router';
import { EmployeeRoutes } from './employee.routes';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [EmployeIndexComponent, EmployeDetailsComponent, EmployeEditComponent],
  imports: [
    RouterModule.forChild(EmployeeRoutes),
    CommonModule,
    CoreModule,
    SharedModule,
    FormsModule
  ]
})
export class EmployeeModule { }
