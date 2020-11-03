import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClarityModule } from '@clr/angular';
import { ListNetworksComponent } from './list-networks/list-networks.component';



@NgModule({
  declarations: [ListNetworksComponent],
  imports: [
    CommonModule,
    ClarityModule
  ]
})
export class NetworksModule { }
