import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from 'src/app/admin/services/admin.service';
import { UserService } from 'src/app/user/services/user.service';

@Component({
  selector: 'app-admin-user-details',
  templateUrl: './admin-user-details.component.html',
  styleUrls: ['./admin-user-details.component.scss']
})
export class AdminUserDetailsComponent implements OnInit {
  pastOrders: any;
  loggedInUser=  this.activatedRoute.snapshot.paramMap.get('name');
  constructor(private userService:UserService, private adminservice: AdminService, private activatedRoute: ActivatedRoute, private router: Router) { }
  userObj: any;
  dupUserData: any;
  isUpdated = false;

  ngOnInit(): void {
    this.userService.getPastOrders(this.loggedInUser).subscribe((res: any) => {
      console.log(res);
      this.pastOrders = res;
    })
    let name: string | null = this.activatedRoute.snapshot.paramMap.get('name');

    this.adminservice.getUserByName(name)
      .subscribe((res: any) => {
        console.log(res);
        this.userObj = res;
      })
  }

  handleEditModalOpen(): void {
    this.isUpdated = false;
    this.dupUserData = { ...this.userObj };
  }

  handleUpdateBook(): void {
    console.log(this.dupUserData);

    this.adminservice.updateBook(this.dupUserData)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {
          this.isUpdated = true;
          this.userObj = res;
        }
      });
  }



}