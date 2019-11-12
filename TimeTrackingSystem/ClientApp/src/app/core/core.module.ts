import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { AuthorizationService, EmployeeGroupService, EmployeeService, RegisterTimeEndpointService, WorkRegisterEventService } from './api.generated';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { SnackbarHelper } from './helpers/snackbar.helper';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { JwtInterceptor } from './interceptors/jwt.interceptor';

@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        RouterModule,
    ],
    declarations: [HeaderComponent, FooterComponent],
    exports: [HeaderComponent, FooterComponent],
    providers: [
        AuthorizationService,
        EmployeeService,
        RegisterTimeEndpointService,
        WorkRegisterEventService,
        EmployeeGroupService,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
})
export class CoreModule { }
