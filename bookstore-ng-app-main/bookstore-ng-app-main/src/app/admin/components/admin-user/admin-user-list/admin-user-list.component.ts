import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/admin/services/admin.service';

@Component({
  selector: 'app-admin-user-list',
  templateUrl: './admin-user-list.component.html',
  styleUrls: ['./admin-user-list.component.scss']
})
export class AdminUserListComponent implements OnInit {


  userList: any[] = [];
  isUpdated: any;

  constructor(private adminservice: AdminService) { }

  ngOnInit(): void {
    this.adminservice.listUsers().subscribe((res: any) => {
      console.log(res);
      this.userList = res;
    })
  }
  handleActivate(name: string | any): void {
    console.log(name);

    this.adminservice.activateUser(name)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {
          this.isUpdated = true;
          this.userList = res;
        }
      });
  }

  handleDeactivate(name: string | any): void {
    console.log(name);

    this.adminservice.deactivateUser(name)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {
          this.isUpdated = true;
          this.userList = res;
        }
      });
  }

}
