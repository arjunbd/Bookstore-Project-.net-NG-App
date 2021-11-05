import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class BookService {
  private bookObj:any[]=[{
    'imageURL':'default',
    'bookTitle':'deafult',
    'bookDescription':'default',
    'bookPrice':'default'
  }];
  private forWishList:any={
    'userName':'',
    'bookTitle':''
  }
  
  private cartItems = new BehaviorSubject(this.bookObj);
  private cartId:string|null='';
  latestCartItemsList:Observable<any>=this.cartItems.asObservable();
  constructor(private http: HttpClient) { }
  listbooks(): any {
    return this.http.get('http://localhost:64107/api/books')
      .pipe(map((res: any) => {
        return res;
      }));
  }
  getBookbyId(id: string | null): any {
    return this.http.get('http://localhost:64107/api/books?bookId=' + id)
      .pipe(map((res: any) => {
        return res;
      }));
  }
  getBookbyISBN(isbn: string | null): any {
    return this.http.get('http://localhost:64107/api/books?isbn=' + isbn)
      .pipe(map((res: any) => {
        return res;
      }));
  }
  getBookbyCategory(catname: string | null): any {
    return this.http.get('http://localhost:64107/api/books?cat_name=' + catname)
      .pipe(map((res: any) => {
        return res;
      }));
  }
  getBookbyName(name: string | null): any {
    return this.http.get('http://localhost:64107/api/books?b_name' + name)
      .pipe(map((res: any) => {
        return res;
      }));
  }
  getBookbyAuthor(authorname: string | null): any {
    return this.http.get('http://localhost:64107/api/books?author_name=' + authorname)
      .pipe(map((res: any) => {
        return res;
      }));
  }
  removeFromCart(bookTitle:any,userName:any):any{
    this.forWishList.userName=userName;
    this.forWishList.bookTitle=bookTitle;
    console.log(this.forWishList);
    return this.http.request('delete', 'http://localhost:64107/api/fromCart', { body: this.forWishList })
  .pipe(map((res: any) => {
    return res;
  }));
  }
  addToWishList(bookTitle:any,Username:any):any{
    this.forWishList.userName=Username;
    this.forWishList.bookTitle=bookTitle;
    console.log(this.forWishList);
    return this.http.post('http://localhost:64107/api/Wishlist',this.forWishList)
  .pipe(map((res: any) => {
    return res;
  }));
  }
  updateCart(pdt:any,pdt1:any):void{
    console.log(pdt);
    this.addtoCart(pdt1).subscribe((res:any)=>{
      console.log(res);
    });
    this.latestCartItemsList.pipe(take(1)).subscribe((eachpdt:any)=>{
      console.log(eachpdt);
      const newCartItemsArr=[...eachpdt,pdt];
      console.log(newCartItemsArr);
      this.cartItems.next(newCartItemsArr);
    });
  }
  addtoCart(pdt:any):any{
    return this.http.post('http://localhost:64107/api/addtoCart',pdt).pipe(map((res: any) => {
      return res;
    }));
  }
getcart(name:any):any{
  return this.http.get('http://localhost:64107/api/Cart/'+name)
  .pipe(map((res: any) => {
    return res;
  }));
}
createOrder(cartid:any,coupon:any):any{
  return this.http.get('http://localhost:64107/api/BookOrders?cartId='+cartid+'&couponName='+coupon)
  .pipe(map((res: any) => {
    return res;
  }));
  
}
getCost(cartid:any):any{
  return this.http.get('http://localhost:64107/api/BookOrders?cartId='+cartid)
  .pipe(map((res: any) => {
    return res;
  }));
}
}
