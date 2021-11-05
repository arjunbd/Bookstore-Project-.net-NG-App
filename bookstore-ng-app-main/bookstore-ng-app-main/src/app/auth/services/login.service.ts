import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private useregister: any = {
    "userName": "sample string 1",
    "userPassword": "sample string 2",
    "userStatus": true,
    "shippingAddress": "sample string 4"
  };
  private loginstatus: boolean = false;
  private adminstatus: boolean = false;
  private userName: string = '';
  private cartId: number = 0;
  private loginlist = new BehaviorSubject(this.loginstatus);
  private userList = new BehaviorSubject(this.userName);
  private adminlist = new BehaviorSubject(this.adminstatus);
  private cartList = new BehaviorSubject(this.cartId);
  constructor(private http: HttpClient) {

  }
  getCartid(username:string):any{
    return this.http.get('http://localhost:64107/api/Cart?userName='+username)
          .pipe(map((res: any) => {
            console.log("here it is"+res)
            return res;
          }));
  }
  login(formData: any): any {
    console.log(formData);

    return this.http.post('http://localhost:64107/api/Login', formData)
      .pipe(map((res: any) => {
        if (res) {
          localStorage.setItem('authToken', res);
          localStorage.setItem('status', '1');
          localStorage.setItem('adminstatus', '0');
          this.loginlist.next(true);
          this.adminlist.next(false);
          return res;
        }
        else {
          localStorage.removeItem('authToken');
          localStorage.removeItem('status');
          localStorage.removeItem('adminstatus');
        }
      }));
  }
  adminlogin(adminForm: any): any {
    console.log(adminForm);
    return this.http.post('http://localhost:64107/api/Admin', adminForm).pipe(map((res: any) => {
      if (res) {
        localStorage.setItem('authToken', adminForm.userName);
        localStorage.setItem('status', '1');
        localStorage.setItem('adminstatus', '1');
        this.loginlist.next(true);
        this.adminlist.next(true);
        return res;
      }
      else {
        localStorage.removeItem('authToken');
        localStorage.removeItem('status');
        localStorage.removeItem('adminstatus');
      }
    }));
  }
  userRegisterservice(formData: any): any {
    this.useregister.userName = formData.userName;
    this.useregister.userPassword = formData.userPassword;
    this.useregister.userStatus = 1;
    this.useregister.shippingAddress = formData.shippingAddress;
    console.log(this.useregister);
    return this.http.post('http://localhost:64107/api/register', this.useregister)
      .pipe(map((res: any) => {
        if (res) {
          console.log(res);
          console.log(res.type)
          return res;
        }
      }));
  }
  get getactivecartId() {
    return this.cartList.asObservable();
  }
  get getuser() {
    if (localStorage.getItem('status') == '0') {

      return this.userList.asObservable();
    }
    else {
      this.userList.next(localStorage.authToken);
      return this.userList.asObservable();
    }
  }
  get isLoggedIn() {
    if (localStorage.length == 0) {
      this.loginlist.next(false);
      return this.loginlist.asObservable();
    } else {
      this.loginlist.next(true);
      return this.loginlist.asObservable();
    }
  }
  get isAdmin() {
    if (localStorage.length == 0) {
      this.adminlist.next(false);
      return this.adminlist.asObservable();
    }
    else if (localStorage.getItem('adminstatus') == '1') {
      this.adminlist.next(true);
      return this.adminlist.asObservable();
    }
    else {
      this.adminlist.next(false);
      return this.adminlist.asObservable();
    }
  }
  get isLogOut() {
    this.loginlist.next(false);
    this.adminlist.next(false);
    localStorage.removeItem('cartId');
    localStorage.removeItem('status');
    localStorage.removeItem('adminstatus');
    localStorage.removeItem('authToken');
    return this.loginlist.asObservable();
  }
  isAuth() {
    if (localStorage.getItem('authToken')) {
      return true;
    } else {
      return false;
    }
  }
  isAdminAuth() {
    if (localStorage.getItem('authToken') && localStorage.getItem('adminstatus')) {
      return true;
    }
    else {
      return false;
    }
  }
}
