import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  //styleUrls: ['./category-details.component.scss']
})
export class CategoryDetailsComponent implements OnInit {

  constructor(private adminservice: AdminService, private activatedRoute: ActivatedRoute, private router: Router) { }

  catObj: any;
  dupCatData: any;
  dupBookData: any[] = [];
  isUpdated = false;
  isDeleted = false;

  ngOnInit(): void {

    let id: string | null = this.activatedRoute.snapshot.paramMap.get('id');

    this.adminservice.getCategorybyId(id)
      .subscribe((res: any) => {
        console.log(res);
        this.catObj = res;
      })
  }

  handleEditModalOpen(): void {
    this.isUpdated = false;
    this.dupCatData = { ...this.catObj };
  }

  handleUpdateCat(): void {
    console.log(this.dupCatData);

    this.adminservice.updateCat(this.dupCatData)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {
          this.isUpdated = true;
          this.catObj = res;
          //          this.router.navigateByUrl(this.activatedRoute.snapshot.queryParams['admin']);
        }
      });
  }

  handleDeleteModalOpen(): void {
    this.dupCatData = { ...this.catObj };
    this.adminservice.getBookbyCategory(this.dupCatData.categoryName)
      .subscribe((res: any) => {
        if (res) {
          this.dupBookData = res;
        }
      });
  }

  handleDeleteCat(): void {

    this.adminservice.deleteCat(this.dupCatData)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {
          this.isDeleted = true;
          this.catObj = res;
        }
      });
  }
}
