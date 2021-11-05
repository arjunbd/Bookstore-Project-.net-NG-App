import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { RouterModule, Routes } from '@angular/router';
import { CategoryDetailsComponent } from './components/category/category-details/category-details.component';
import { AddCategoryComponent } from './components/category/add-category/add-category.component';
import { AddBookComponent } from './components/books/add-book/add-book.component';
import { BookListComponent } from './components/books/book-list/book-list.component';
import { BookDetailsComponent } from '../books/components/book-details/book-details.component';
import { AdminBookDetailsComponent } from './components/books/admin-book-details/admin-book-details.component';
import { AdminmainComponent } from './components/adminmain/adminmain.component';
import { AdminUserListComponent } from './components/admin-user/admin-user-list/admin-user-list.component';
import { AdminUserDetailsComponent } from './components/admin-user/admin-user-details/admin-user-details.component';
import { AdminCouponListComponent } from './components/admin-coupon/admin-coupon-list/admin-coupon-list.component';
import { AdminAddCouponComponent } from './components/admin-coupon/admin-add-coupon/admin-add-coupon.component';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { AdminGuard } from './guard/admin.guard';
import { PastOrderDetailsComponent } from '../user/components/past-order-details/past-order-details.component';

const adminRoutes: Routes = [
  { path: '', component: AdminmainComponent,canActivate:[AdminGuard] },
  { path: 'category-list', component: CategoryListComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'category-list/add', component: AddCategoryComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'category-list/:id', component: CategoryDetailsComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'book-list', component: BookListComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'book-list/add', component: AddBookComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'book-list/:id', component: AdminBookDetailsComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'user-list', component: AdminUserListComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'user-list/:name', component: AdminUserDetailsComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'user-list/:name/:id', component: PastOrderDetailsComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'coupon-list', component: AdminCouponListComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'coupon-list/add', component: AdminAddCouponComponent, outlet: 'test',canActivate:[AdminGuard] },
  { path: 'admin-home', component: AdminHomeComponent, outlet: 'test' ,canActivate:[AdminGuard]}
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(adminRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AdminRoutingModule { }
