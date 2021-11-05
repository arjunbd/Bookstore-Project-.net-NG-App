import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {

  categoryList: any[] = [];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.adminService.listCategories().subscribe((res:any) => {
      console.log(res);
      this.categoryList = res;
    })
  }

}

