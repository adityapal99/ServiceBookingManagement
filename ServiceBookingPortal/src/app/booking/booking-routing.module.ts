import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './request/create/create.component';
import { DetailsComponent } from './request/details/details.component';
import { EditComponent } from './request/edit/edit.component';
import { IndexComponent } from './request/index/index.component';

const routes: Routes = [
  { path: 'request', redirectTo: 'request/index', pathMatch: 'full'},
  { path: 'request/index', component: IndexComponent },
  { path: 'request/details/:requestId', component: DetailsComponent },
  { path: 'request/create', component: CreateComponent },
  { path: 'request/edit/:requestId', component: EditComponent } 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookingRoutingModule { }
