import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { AppRootRoutes } from './app.routes';
import { API_BASE_URL } from './core/api.generated';
import { CoreModule } from './core/core.module';
import { AuthGuard } from './core/guards/auth.guard';
import { EmployeeModule } from './modules/employee/employee.module';
import { HomeComponent } from './modules/main/home/home.component';
import { LoginComponent } from './modules/main/login/login.component';
import { MainModule } from './modules/main/main.module';
import { SharedModule } from './shared/shared.module';



@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,
        CoreModule,
        MainModule,
        SharedModule,
        EmployeeModule,
        RouterModule.forRoot(AppRootRoutes),
    ],
    providers: [{ provide: API_BASE_URL, useValue: ' ' },
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
