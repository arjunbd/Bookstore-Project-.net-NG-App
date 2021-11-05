import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-past-order-details',
  templateUrl: './past-order-details.component.html',
  styles: [
  ]
})
export class PastOrderDetailsComponent implements OnInit {

  pastOrderDetails: any;

  constructor(private userService: UserService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    let orderId: string | null = this.activatedRoute.snapshot.paramMap.get('id');

    this.userService.getPastOrderDetails(orderId).subscribe((res: any) => {
      console.log(res);
      this.pastOrderDetails = res;
    });

  }

}
