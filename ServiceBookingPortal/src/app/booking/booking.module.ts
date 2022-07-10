import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookingRoutingModule } from './booking-routing.module';
import { IndexComponent } from './request/index/index.component';
import { CreateComponent } from './request/create/create.component';
import { DetailsComponent } from './request/details/details.component';
import { EditComponent } from './request/edit/edit.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IndexComponent as RequestIndex } from './report/index/index.component';
import { CreateComponent as RequestCreate} from './report/create/create.component';
import { DetailsComponent as RequestDetails } from './report/details/details.component';
import { EditComponent as RequestEdit } from './report/edit/edit.component';
import { ToastrService } from 'ngx-toastr';


@NgModule({
  declarations: [
    IndexComponent,
    CreateComponent,
    DetailsComponent,
    EditComponent,
    RequestCreate,
    RequestDetails,
    RequestEdit,
    RequestIndex
  ],
  imports: [
    CommonModule,
    BookingRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    ToastrService
  ]
})
export class BookingModule { }
