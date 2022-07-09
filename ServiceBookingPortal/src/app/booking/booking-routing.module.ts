import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './request/create/create.component';
import { DetailsComponent } from './request/details/details.component';
import { EditComponent } from './request/edit/edit.component';
import { IndexComponent } from './request/index/index.component';
import { CreateComponent as ReportCreate } from './report/create/create.component';
import { DetailsComponent as ReportDetails } from './report/details/details.component';
import { EditComponent as ReportEdit } from './report/edit/edit.component';
import { IndexComponent as ReportIndex } from './report/index/index.component';

const routes: Routes = [
  { path: 'request', redirectTo: 'request/index', pathMatch: 'full'},
  { path: 'request/index', component: IndexComponent },
  { path: 'request/details/:requestId', component: DetailsComponent },
  { path: 'request/create', component: CreateComponent },
  { path: 'request/edit/:requestId', component: EditComponent } ,
  { path: 'report', redirectTo: 'report/index', pathMatch: 'full'},
  { path: 'report/index', component: ReportIndex },
  { path: 'report/details/:reportId', component: ReportDetails },
  { path: 'report/create', component: ReportCreate },
  { path: 'report/edit/:reportId', component: ReportEdit }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookingRoutingModule { }
