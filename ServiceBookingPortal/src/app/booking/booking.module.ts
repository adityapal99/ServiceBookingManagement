import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookingRoutingModule } from './booking-routing.module';
import { IndexComponent } from './request/index/index.component';
import { CreateComponent } from './request/create/create.component';
import { DetailsComponent } from './request/details/details.component';
import { EditComponent } from './request/edit/edit.component';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    IndexComponent,
    CreateComponent,
    DetailsComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    BookingRoutingModule,
    HttpClientModule
  ]
})
export class BookingModule { }
