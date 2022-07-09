import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './authentication/auth.service';
import { AuthInterceptor } from './authentication.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { HomeComponent } from './home/home.component';
import { ProductModule } from './product/product.module';
import { UsersModule } from './users/users.module';
import { BookingModule } from './booking/booking.module';
import { AuthenticationModule } from './authentication/authentication.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    AuthenticationModule,
    ProductModule,
    UsersModule,
    BookingModule
  ],
  providers: [
    AuthService,
    ToastrService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
