import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { ApiEscalaComponent } from './home/api-escala/api-escala.component';
import { ApiLocalComponent } from './home/api-local/api-local.component';
import { ApiMarcacaoEscalaComponent } from './home/api-marcacao-escala/api-marcacao-escala.component';
import { ApiPolicialComponent } from './home/api-policial/api-policial.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    ApiEscalaComponent,
    ApiLocalComponent,
    ApiMarcacaoEscalaComponent,
    ApiPolicialComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
