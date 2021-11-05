import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private login: LoginService, private router: Router, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {

  }
  handleLogin(formData: any): void {

    this.login.login(formData.value)
      .subscribe((res: any) => {
        console.log(res);
        if (res) {    //login condition is to be set.
          this.login.getCartid(formData.value.userName)
            .subscribe((res: any) => {
              console.log(res);
              localStorage.setItem("cartId", res);
            })
          alert("Login Successful!");
          console.log(localStorage.authToken)
          this.router.navigateByUrl(this.activatedRoute.snapshot.queryParams['returnURL']);
        }
        else {
          alert("Login UnSuccessful!");
        }
      })
  }
}
