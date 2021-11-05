import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminloginComponent } from './auth/components/adminlogin/adminlogin.component';
import { LoginComponent } from './auth/components/login/login.component';
import { ResetPwComponent } from './auth/components/reset-pw/reset-pw.component';
import { SignupComponent } from './auth/components/signup/signup.component';
import { LinkGuard } from './auth/guard/link.guard';
import { BookDetailsComponent } from './books/components/book-details/book-details.component';
import { ListBooksComponent } from './books/components/list-books/list-books.component';
import { HomeComponent } from './home/components/home/home.component';
import { CartComponent } from './shopping/components/cart/cart.component';
import { CheckoutComponent } from './shopping/components/checkout/checkout.component';
import { ConfirmationComponent } from './shopping/components/confirmation/confirmation.component';
import { PastOrderDetailsComponent } from './user/components/past-order-details/past-order-details.component';
import { PastOrdersComponent } from './user/components/past-orders/past-orders.component';
import { ProfileComponent } from './user/components/profile/profile.component';
import { WishlistComponent } from './user/components/wishlist/wishlist.component';
const routes: Routes = [
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
  },
  { path: '', component: HomeComponent},
  { path: 'login', component: LoginComponent},
  {path:'adminlogin',component:AdminloginComponent},
  { path: 'signup', component: SignupComponent},
  { path: 'reset-pw', component: ResetPwComponent},
  { path: 'books', component: ListBooksComponent,canActivate:[LinkGuard]},
  { path: 'books/:id', component: BookDetailsComponent,canActivate:[LinkGuard]},
  { path: 'cart', component: CartComponent,canActivate:[LinkGuard]},
  { path: 'checkout', component: CheckoutComponent,canActivate:[LinkGuard]},
  { path: 'confirmation', component: ConfirmationComponent,canActivate:[LinkGuard]},
  { path: 'profile', component: ProfileComponent},
  { path: 'wishlist', component: WishlistComponent},
  { path: 'past-orders', component: PastOrdersComponent},
  { path: 'past-orders/:id', component: PastOrderDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
 