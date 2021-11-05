import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from 'src/app/auth/services/login.service';
import { BookService } from 'src/app/books/services/book.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  cartItemList:any[]=[];
  cartId:string|null='';
  totalcost:any|undefined;
  isLoggedIn:Observable<boolean> | undefined
  constructor(private login :LoginService,private bookservice:BookService,private router :Router) {
    
   }
  ngOnInit(): void {
    this.cartId=localStorage.getItem('cartId');
    this.bookservice.getcart(this.cartId).subscribe((res:any)=>{

      console.log(res);
      this.cartItemList=res;
    })
    this.bookservice.getCost(this.cartId).subscribe((res:any)=>{

      console.log(res);
      this.totalcost=res;
    })
    
  }
  acceptOrder():void{
      alert("order successfull");
      this.isLoggedIn=this.login.isLogOut;
      this.router.navigateByUrl('/books');
  }

}
