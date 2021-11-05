import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styles: [
  ]
})
export class AddCategoryComponent implements OnInit {

  isSaved = false;


  addCategoryForm = new FormGroup({
    categoryName: new FormControl('', Validators.required),
    categoryDescription: new FormControl('', Validators.required),
    //categoryImage: new FormControl('a@b.com'),
    categoryStatus: new FormControl(''),
    categoryPosition: new FormControl('', Validators.required),

  });


  constructor(private adminservice: AdminService) { }

  ngOnInit(): void {
  }

  handleAddCategory(): void {
    console.log('Submitting');

    //console.log(this.addUserForm); // the entire form state

    // Read form data here
    console.log(this.addCategoryForm.value);

    // 2. send the above data to the service
    this.adminservice.createCategory(this.addCategoryForm.value)
      .subscribe((res: any) => { // 3. get the response from service
        console.log(res);
        if (res) {
          this.isSaved = true;
        }
      });
  }

}