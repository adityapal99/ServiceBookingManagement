import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { IndexComponent } from './index/index.component';



const routes: Routes = [
  { path: 'users', component: IndexComponent },
  { path: 'users/details/:id', component: DetailsComponent },
  { path: 'users/edit/:userId', component: EditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
