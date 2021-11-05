import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginService } from 'src/app/auth/services/login.service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.scss']
})
export class AdminHomeComponent implements OnInit {
  isLoggedIn:Observable<boolean> | undefined
  isUser:Observable<string>|undefined
  constructor(private login:LoginService) {
    this.isLoggedIn=this.login.isLoggedIn;
    this.isUser=this.login.getuser;
   }

  ngOnInit(): void {
  }

}
