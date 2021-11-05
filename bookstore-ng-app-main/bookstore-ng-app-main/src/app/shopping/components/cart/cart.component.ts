import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from 'src/app/auth/services/login.service';
import { BookService } from 'src/app/books/services/book.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  ActivecartId:Observable<number>|undefined;
cartItemList:any[]=[];
userId:string|null='';
cartId:string|null='';
isUser:Observable<string>|undefined
  constructor(private login:LoginService,private bookservice:BookService,private router:Router) {
    
   }

  ngOnInit(): void {
    this.userId=localStorage.getItem('authToken');
    console.log(this.userId)
    this.isUser=this.login.getuser;
    this.cartId=localStorage.getItem('cartId');
    this.bookservice.getcart(this.cartId).subscribe((res:any)=>{

      console.log(res);
      this.cartItemList=res;
    })
  }
  handleremoveCart($event: any): void {
    let bookTitle: string = $event.srcElement.id;
    let userName:string|null=localStorage.getItem('authToken');
    this.bookservice.removeFromCart(bookTitle,userName).subscribe((res: any) => {
      console.log(res);
      if (res) {
        this.bookservice.getcart(this.cartId).subscribe((res: any) => {
          console.log(res);
          this.cartItemList = res;
        });
      }
  });
}
  handleOrder(formData: any): void {
    console.log(formData.value)
    this.bookservice.createOrder(this.cartId,formData.value.Coupon) .subscribe((res:any)=>{
      if(res){
        this.router.navigateByUrl('checkout');
      }
    })
  }
}
