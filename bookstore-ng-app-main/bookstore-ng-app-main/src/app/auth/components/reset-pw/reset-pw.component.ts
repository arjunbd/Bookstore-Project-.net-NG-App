import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-reset-pw',
  templateUrl: './reset-pw.component.html',
  styleUrls: ['./reset-pw.component.scss']
})
export class ResetPwComponent implements OnInit {
  constructor(private login:LoginService) {
    
   }

  ngOnInit(): void {
  }

}
