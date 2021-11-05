import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginService } from 'src/app/auth/services/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  isLoggedIn:Observable<boolean> | undefined
  isUser:Observable<string>|undefined
  constructor(private login:LoginService) {
   
   }

  ngOnInit(): void {
    this.isLoggedIn=this.login.isLoggedIn;
    this.isUser=this.login.getuser;
  }

}
