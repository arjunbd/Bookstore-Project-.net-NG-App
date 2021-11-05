import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/admin/services/admin.service';

@Component({
  selector: 'app-admin-add-coupon',
  templateUrl: './admin-add-coupon.component.html',
  styles: [
  ]
})
export class AdminAddCouponComponent implements OnInit {

  isSaved = false;

  addCouponForm = new FormGroup({
    couponName: new FormControl('', Validators.required),
    discountRate: new FormControl('', Validators.required),

  });

  constructor(private adminservice: AdminService) { }

  ngOnInit(): void {
  }

  handleAddCoupon(): void {
    console.log('Submitting');

    //console.log(this.addUserForm); // the entire form state

    // Read form data here
    console.log(this.addCouponForm.value);

    // 2. send the above data to the service
    this.adminservice.createCoupon(this.addCouponForm.value)
      .subscribe((res: any) => { // 3. get the response from service
        console.log(res);
        if (res) {
          this.isSaved = true;
        }
      });
  }

}
