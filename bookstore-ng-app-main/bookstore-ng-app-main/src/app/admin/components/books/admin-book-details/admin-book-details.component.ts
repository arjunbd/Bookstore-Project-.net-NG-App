import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from 'src/app/admin/services/admin.service';

@Component({
  selector: 'app-admin-book-details',
  templateUrl: './admin-book-details.component.html',
  styleUrls: ['./admin-book-details.component.scss']
})
export class AdminBookDetailsComponent implements OnInit {

  constructor(private adminservice: AdminService, private activatedRoute: ActivatedRoute, private router: Router) { }

  bookObj: any;
  dupBookData: any;
  isUpdated = false;
  isDeleted = false;

  ngOnInit(): void {

    let id: string | null = this.activatedRoute.snapshot.paramMap.get('id');

    this.adminservice.getBookbyId(id)
      .subscribe((res: any) => {
        console.log(res);
        this.bookObj = res;
      })
  }

  handleEditModalOpen(): void {
    this.isUpdated = false;
    this.dupBookData = { ...this.bookObj };
  }

  handleUpdateBook(): void {
    console.log(this.dupBookData);

    this.adminservice.updateBook(this.dupBookData)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {
          this.isUpdated = true;
          this.bookObj = res;
          //          this.router.navigateByUrl(this.activatedRoute.snapshot.queryParams['admin']);
        }
      });
  }
}
