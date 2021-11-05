import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { LoginService } from 'src/app/auth/services/login.service';
import { BookService } from '../../services/book.service';
@Component({
  selector: 'app-list-books',
  templateUrl: './list-books.component.html',
  styleUrls: ['./list-books.component.scss']
})
export class ListBooksComponent implements OnInit {
  bookList:any[]=[];
  UserId:string|null=''
  constructor(private bookservice:BookService,private login:LoginService) {
    
   }
  
  HandleSearch():void{
    console.log(this.SearchByForm.value)
    // searchby: '43255525', searchquery: 'ISBN'
    if(this.SearchByForm.value.searchquery=='ISBN')
    {
      this.bookservice.getBookbyISBN(this.SearchByForm.value.searchby).subscribe((res:any)=>{
        console.log(res);
        this.bookList=res;
        
      })
      console.log();
    }
    else if(this.SearchByForm.value.searchquery=='Category')
    {
      this.bookservice.getBookbyCategory(this.SearchByForm.value.searchby).subscribe((res:any)=>{
        console.log(res);
        this.bookList=res;
      })
      console.log();
    }
    else if(this.SearchByForm.value.searchquery=='Author')
    {
      this.bookservice.getBookbyAuthor(this.SearchByForm.value.searchby).subscribe((res:any)=>{
        console.log(res);
        this.bookList=res;
      })
      console.log();
    }
    else
    {
      this.bookservice.getBookbyName(this.SearchByForm.value.searchby).subscribe((res:any)=>{
        console.log(res);
        this.bookList=res;
      })
      console.log();
    }
  }
  ngOnInit(): void {
    this.UserId=localStorage.getItem('authToken');
    this.bookservice.listbooks().subscribe((res:any)=>{

      console.log(res);
      this.bookList=res;
    })
  }
  SearchByForm =new FormGroup({
    searchby:new FormControl('Search Here'),
    searchquery:new FormControl('Search By'),
  });

}
