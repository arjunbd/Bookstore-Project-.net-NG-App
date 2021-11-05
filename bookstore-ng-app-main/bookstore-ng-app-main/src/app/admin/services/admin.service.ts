import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  // Book Functions 

  listbooks(): any {
    return this.http.get('http://localhost:64107/api/books')
      .pipe(map((res: any) => {
        return res;
      }));
  }

  getBookbyId(id: string | null): any {
    return this.http.get('http://localhost:64107/api/books?bookId=' + id)
      .pipe(map((res: any) => {
        return res;
      }));
  }

  createBook(formData: any): any {
    console.log(formData);
    return this.http.post('http://localhost:64107/api/addBook', formData)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }

  updateBook(formData: any): any {
    console.log(formData);
    let updateURL = `http://localhost:64107/api/Books?bookId=${formData.bookId}`;

    return this.http.put(updateURL, formData)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }

  // Category Functions

  listCategories(): any {
    return this.http.get('http://localhost:64107/api/Category')
      .pipe(map((res: any) => {
        return res;
      }));
  }

  getCategorybyId(id: string | null): any {
    return this.http.get('http://localhost:64107/api/Category/' + id)
      .pipe(map((res: any) => {
        return res;
      }));
  }

  createCategory(formData: any): any {
    console.log(formData);
    return this.http.post('http://localhost:64107/api/category', formData)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }

  updateCat(formData: any): any {
    console.log(formData);
    let updateURL = `http://localhost:64107/api/Category?catId=${formData.categoryId}`;

    return this.http.put(updateURL, formData)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }

  getBookbyCategory(catname: string | null): any {
    return this.http.get('http://localhost:64107/api/books?cat_name=' + catname)
      .pipe(map((res: any) => {
        return res;
      }));
  }

  deleteCat(formData: any): any {
    console.log(formData);
    let deleteURL = `http://localhost:64107/api/Category?catId=${formData.categoryId}`;

    return this.http.delete(deleteURL)
      .pipe(map((res: any) => {
        console.log(res);
        return res;
      }));
  }
//User Functions

listUsers(): any {
  return this.http.get('http://localhost:64107/api/AllUsers')
    .pipe(map((res: any) => {
      return res;
    }));
}

getUserByName(name: string | null): any {
  return this.http.get('http://localhost:64107/api/user?userName=' + name)
    .pipe(map((res: any) => {
      return res;
    }));
}

activateUser(name: string | null): any {
  return this.http.get('http://localhost:64107/api/Admin?ActivateUserName=' + name)
    .pipe(map((res: any) => {
      return res;
    }));
}

deactivateUser(name: string | null): any {
  return this.http.get('http://localhost:64107/api/Admin?deactivateUserName=' + name)
    .pipe(map((res: any) => {
      return res;
    }));
}

//Coupon Functions

listCoupons(): any {
  return this.http.get('http://localhost:64107/api/coupon')
    .pipe(map((res: any) => {
      return res;
    }));
}

createCoupon(formData: any): any {
  console.log(formData);
  return this.http.get(`http://localhost:64107/api/Admin?couponName=${formData.couponName}&discountRate=${formData.discountRate}`)
    .pipe(map((res: any) => {
      console.log(res);
      return res;
    }));
}

}
