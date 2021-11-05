import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/auth/services/login.service';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.scss']
})
export class ConfirmationComponent implements OnInit {

  constructor(private login:LoginService) {
  
   }

  ngOnInit(): void {
  }

}
