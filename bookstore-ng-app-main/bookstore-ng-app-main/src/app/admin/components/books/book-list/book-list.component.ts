import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/admin/services/admin.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {

  bookList: any[] = [];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.adminService.listbooks().subscribe((res:any) => {
      console.log(res);
      this.bookList = res;
    })
  }

}
