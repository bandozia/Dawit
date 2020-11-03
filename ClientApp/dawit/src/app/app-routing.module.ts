import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './home/login/login.component';
import { RegisterComponent } from './home/register/register.component';
import { ListNetworksComponent } from './networks/list-networks/list-networks.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent },
  {path: 'register', component: RegisterComponent },
  {path: 'networks', component: ListNetworksComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
