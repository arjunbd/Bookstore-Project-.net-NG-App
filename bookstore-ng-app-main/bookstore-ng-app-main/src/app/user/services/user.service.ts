import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUserDetails(userName: string | null): any {
    return this.http.get('http://localhost:64107/api/user?userName=' + userName)
      .pipe(map((res: any) => {
        console.log(res)
        return res;
      }));
  }

  getWishList(userName: string | null): any {
    return this.http.get('http://localhost:64107/api/wishlist?userName=' + userName)
      .pipe(map((res: any) => {
        return res;
      }));
  }

  updateShippingAddress(formData: any): any {
    console.log(formData);
    let updateURL = `http://localhost:64107/api/user?userName=${formData.userName}&newShippingAddress=${formData.shippingAddress}`;

    return this.http.get(updateURL)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }
  deleteFromWishlist(userName: string, bookTitle: string): any {
    let requestBody = {
      userName: userName,
      bookTitle: bookTitle
    };
    return this.http.request('delete', 'http://localhost:64107/api/Wishlist', { body: requestBody })
      .pipe(map((res: any) => {
        return res;
      }));
  }
  getPastOrders(userName: string | null): any {
    return this.http.get('http://localhost:64107/api/bookOrders?userName=' + userName)
      .pipe(map((res: any) => {
        return res;
      }));
  }

  getPastOrderDetails(orderId: string | null): any {
    return this.http.get('http://localhost:64107/api/bookOrders?orderId=' + orderId)
      .pipe(map((res: any) => {
        return res;
      }));
  }

}
