import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-past-orders',
  templateUrl: './past-orders.component.html',
  styles: [
  ]
})
export class PastOrdersComponent implements OnInit {

  pastOrders: any;
  loggedInUser: any | string = localStorage.getItem('authToken');

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getPastOrders(this.loggedInUser).subscribe((res: any) => {
      console.log(res);
      this.pastOrders = res;
    })
    console.log(this.pastOrders);
  }


}
