import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-adminlogin',
  templateUrl: './adminlogin.component.html',
  styles: [
  ]
})
export class AdminloginComponent implements OnInit {

  constructor(private login: LoginService, private router: Router) { }

  ngOnInit(): void {

  }
  handleAdminLogin(adminForm: any): any {
    console.log(adminForm);
    this.login.adminlogin(adminForm.value)
    .subscribe((res: any) => {
      console.log(res);
      if (res) {    //login condition is to be set.
        alert("Login Successful!");
        console.log(localStorage.authToken)
        this.router.navigateByUrl('admin/(test:admin-home)');
      }
      else {
        alert("Login UnSuccessful!");
        localStorage.setItem('status','0');
        localStorage.setItem('adminstatus','0');
      }
    })
  }
}
