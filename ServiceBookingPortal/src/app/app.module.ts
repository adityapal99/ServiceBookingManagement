import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './authentication/login/login.component';
import { FormBuilder, ReactiveFormsModule,FormsModule } from '@angular/forms';
import { AuthenticationModule } from './authentication/authentication.module';
import { AuthService } from './authentication/auth.service';
import { AuthInterceptor } from './authentication.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { HomeComponent } from './home/home.component';
import { ProductModule } from './product/product.module';
import { BookingModule } from './booking/booking.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
<<<<<<< HEAD
    ProductModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
=======
    AuthenticationModule,
    ProductModule,
    BookingModule
>>>>>>> afaec685bd416029195137810022da23d036ac53
  ],
  providers: [
    AuthService,
    AuthInterceptor
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
