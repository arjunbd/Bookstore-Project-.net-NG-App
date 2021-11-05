import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { CategoryDetailsComponent } from './components/category/category-details/category-details.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddCategoryComponent } from './components/category/add-category/add-category.component';
import { AddBookComponent } from './components/books/add-book/add-book.component';
import { BookListComponent } from './components/books/book-list/book-list.component';
import { AdminBookDetailsComponent } from './components/books/admin-book-details/admin-book-details.component';
import { AdminmainComponent } from './components/adminmain/adminmain.component';
import { NavadminComponent } from './components/navadmin/navadmin.component';
import { AdminUserListComponent } from './components/admin-user/admin-user-list/admin-user-list.component';
import { AdminUserDetailsComponent } from './components/admin-user/admin-user-details/admin-user-details.component';
import { AdminCouponListComponent } from './components/admin-coupon/admin-coupon-list/admin-coupon-list.component';
import { AdminAddCouponComponent } from './components/admin-coupon/admin-add-coupon/admin-add-coupon.component';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';


@NgModule({
  declarations: [
    CategoryListComponent,
    CategoryDetailsComponent,
    AddCategoryComponent,
    AddBookComponent,
    BookListComponent,
    AdminBookDetailsComponent,
    AdminmainComponent,
    NavadminComponent,
    AdminUserListComponent,
    AdminUserDetailsComponent,
    AdminCouponListComponent,
    AdminAddCouponComponent,
    AdminHomeComponent,

  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AdminModule { }
