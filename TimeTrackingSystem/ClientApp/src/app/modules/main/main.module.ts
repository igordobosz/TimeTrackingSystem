import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../../core/core.module';
import { SharedModule } from '../../shared/shared.module';
import { EndpointComponent } from './endpoint/endpoint.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { MainRoutes } from './main.routes';



@NgModule({
    declarations: [HomeComponent, LoginComponent, EndpointComponent],
    imports: [
        RouterModule.forChild(MainRoutes),
        CommonModule,
        CoreModule,
        SharedModule,
    ],
})
export class MainModule { }
