import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {

  wishList: any;
  deletedFromWishlist = false;
  loggedInUser: any|string = localStorage.getItem('authToken');

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getWishList(this.loggedInUser).subscribe((res: any) => {
      console.log(res);
      this.wishList = res;
    });
  }

  handleDeleteFromWishlist($event: any): void {

    let bookTitle: string = $event.srcElement.id;
    console.log(bookTitle);

    this.userService.deleteFromWishlist(this.loggedInUser, bookTitle)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {
          this.userService.getWishList(this.loggedInUser).subscribe((res: any) => {
            console.log(res);
            this.wishList = res;
          });
          this.deletedFromWishlist = true;
        }
      });
  }

}
