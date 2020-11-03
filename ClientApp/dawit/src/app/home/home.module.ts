import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ClarityModule } from '@clr/angular';
import { HeaderComponent } from './header/header.component';
import { ReactiveFormsModule } from '@angular/forms';
import { GlobalAlertsComponent } from './global-alerts/global-alerts.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [LoginComponent, RegisterComponent, HeaderComponent, GlobalAlertsComponent],
  imports: [
    CommonModule,
    RouterModule,
    ClarityModule,
    ReactiveFormsModule
  ],
  exports: [
    HeaderComponent,
    GlobalAlertsComponent
  ]
})
export class HomeModule { }
