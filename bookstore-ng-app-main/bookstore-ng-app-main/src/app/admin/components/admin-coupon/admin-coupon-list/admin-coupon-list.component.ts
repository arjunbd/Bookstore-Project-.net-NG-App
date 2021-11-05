import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/admin/services/admin.service';

@Component({
  selector: 'app-admin-coupon-list',
  templateUrl: './admin-coupon-list.component.html',
  styles: [
  ]
})
export class AdminCouponListComponent implements OnInit {

  couponList: any[] = [];
  constructor(private adminservice: AdminService) { }

  ngOnInit(): void {
    this.adminservice.listCoupons().subscribe((res:any) => {
      console.log(res);
      this.couponList = res;
    })
  }

}
