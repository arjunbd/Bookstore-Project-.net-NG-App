import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginService } from 'src/app/auth/services/login.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  isLoggedIn:Observable<boolean> | undefined
  isAdmin:Observable<boolean>|undefined
  constructor(private login:LoginService) {
     
  }
  ngOnInit(): void {
    this.isLoggedIn=this.login.isLoggedIn;
    this.isAdmin=this.login.isAdmin;
    this.isLoggedIn.subscribe(res => console.log(res));
    this.isAdmin.subscribe(res => console.log(res));
  }

  Logout() {
    this.isLoggedIn=this.login.isLogOut;
    console.log(this.isLoggedIn)
  }

}
