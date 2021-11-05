import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoginService } from 'src/app/auth/services/login.service';
import { BookService } from '../../services/book.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent implements OnInit {

  constructor(private login:LoginService, private bookservice:BookService,private route:ActivatedRoute) {
   
   }
  bookObj:any={
    'imageURL':'',
    'bookTitle':'',
    'bookDescription':'',
    'bookPrice':''
  }
  cartObj:any={
    "bookId": '0',
  "orderQty": '1',
  "cartId": '1'
  }
  userName:any=''
  ngOnInit(): void {
    this.userName=localStorage.getItem('authToken');
    let id:string|null=this.route.snapshot.paramMap.get('id');
    this.bookservice.getBookbyId(id).subscribe((res:any)=>{

      console.log(res);
      this.bookObj.imageURL=res.bookImage;
      this.bookObj.bookTitle=res.bookTitle;
      this.bookObj.bookDescription=res.bookDescription;
      this.bookObj.bookPrice=res.bookPrice;
      this.cartObj.bookId=id;
      this.cartObj.cartId=localStorage.getItem('cartId');
    })
  }
  AddtoWishList(book:any,userName:any):void{
    this.bookservice.addToWishList(book.bookTitle,userName).subscribe((res:any)=>{
     console.log(res);
     alert("added to wishList")
   })
  }
  handleAddtoCArt(pdt:any,pdt1:any):void{
    
 console.log(pdt);
 alert("added to The cart")
 this.bookservice.updateCart(pdt,pdt1);
  }
}
