import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  constructor(private login:LoginService,private router:Router) { 
    
  }

  ngOnInit(): void {
  }
  handleRegister(regForm:NgForm):void{
    console.log(regForm.value);
    this.login.userRegisterservice(regForm.value) .subscribe( (res: any)=> {
      console.log(res);
      if(res==1){
        alert("signup success");
        this.router.navigate(['/login'])
      }
    });
  }
}

