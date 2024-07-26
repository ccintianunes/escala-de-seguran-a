import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { ApiEscalaComponent } from './home/api-escala/api-escala.component';
import { ApiLocalComponent } from './home/api-local/api-local.component';
import { ApiMarcacaoEscalaComponent } from './home/api-marcacao-escala/api-marcacao-escala.component';
import { ApiPolicialComponent } from './home/api-policial/api-policial.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: DashboardComponent },
  { path: 'home/api-escala', component: ApiEscalaComponent },
  { path: 'home/api-local', component: ApiLocalComponent },
  { path: 'home/api-marcacao-escala', component: ApiMarcacaoEscalaComponent },
  { path: 'home/api-policial', component: ApiPolicialComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
