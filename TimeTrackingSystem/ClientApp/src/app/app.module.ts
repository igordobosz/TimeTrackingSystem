import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './modules/main/home/home.component';
import { MainModule } from './modules/main/main.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginComponent } from './modules/main/login/login.component';
import { AppRootRoutes } from './app.routes';



@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CoreModule,
    MainModule,
    SharedModule,
    RouterModule.forRoot(AppRootRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
