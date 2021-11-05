import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/admin/services/admin.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.scss']
})
export class AddBookComponent implements OnInit {

  isSaved = false;


  addBookForm = new FormGroup({
    bookTitle: new FormControl('', Validators.required),
    categoryId: new FormControl('', Validators.required),
    bookISBN: new FormControl('', Validators.required),
    author: new FormControl('', Validators.required),
    bookYear: new FormControl('', Validators.required),
    bookPrice: new FormControl('', Validators.required),
    bookDescription: new FormControl('', Validators.required),
    bookImage: new FormControl("../../../../assets/images/harryPotter.jpg"),
    bookStatus: new FormControl(''),
    bookPosition: new FormControl('', Validators.required),

  });


  constructor(private adminservice: AdminService,private router:Router) { }

  ngOnInit(): void {
  }

  handleAddBook(): void {
    console.log('Submitting');

    //console.log(this.addUserForm); // the entire form state

    // Read form data here
    console.log(this.addBookForm.value);

    // 2. send the above data to the service
    this.adminservice.createBook(this.addBookForm.value)
      .subscribe((res: any) => { // 3. get the response from service
        console.log(res);
        if (res) {
          this.isSaved = true;
        }
      });
  }
  close(){
    this.router.navigate([{outlets:{test:null}}])
  }

}