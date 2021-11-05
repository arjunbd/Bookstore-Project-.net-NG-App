import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/header/header.component';
import { FooterComponent } from './shared/footer/footer.component';
import { HomeComponent } from './home/components/home/home.component';
import { ListBooksComponent } from './books/components/list-books/list-books.component';
import { BookDetailsComponent } from './books/components/book-details/book-details.component';
import { LoginComponent } from './auth/components/login/login.component';
import { SignupComponent } from './auth/components/signup/signup.component';
import { ResetPwComponent } from './auth/components/reset-pw/reset-pw.component';
import { CartComponent } from './shopping/components/cart/cart.component';
import { CheckoutComponent } from './shopping/components/checkout/checkout.component';
import { ConfirmationComponent } from './shopping/components/confirmation/confirmation.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminloginComponent } from './auth/components/adminlogin/adminlogin.component';
import { ProfileComponent } from './user/components/profile/profile.component';
import { WishlistComponent } from './user/components/wishlist/wishlist.component';
import { PastOrderDetailsComponent } from './user/components/past-order-details/past-order-details.component';
import { PastOrdersComponent } from './user/components/past-orders/past-orders.component';
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    ListBooksComponent,
    BookDetailsComponent,
    LoginComponent,
    SignupComponent,
    ResetPwComponent,
    CartComponent,
    CheckoutComponent,
    ConfirmationComponent,
    AdminloginComponent,
    ProfileComponent,
    WishlistComponent,
    PastOrderDetailsComponent,
    PastOrdersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
