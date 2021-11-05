import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  userDetails: any;
  dupUserDetails: any;
  isUpdated = false;
  loggedInUser: any|string = localStorage.getItem('authToken');

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUserDetails(this.loggedInUser).subscribe((res: any) => {
      console.log(res);
      this.userDetails = res;
      this.dupUserDetails = this.userDetails;
    });
  }

  handleUpdateShippingAddress(): void {
    console.log(this.dupUserDetails);

    this.userService.updateShippingAddress(this.dupUserDetails)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {
          this.isUpdated = true;
        }
      });
  }

}
