import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GetDetailsComponent } from './Component/get-details/get-details.component';
import { TicketFormComponent } from './Component/ticket-form/ticket-form.component';

const routes: Routes = [
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
