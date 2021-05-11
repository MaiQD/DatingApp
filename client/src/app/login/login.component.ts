import { error } from 'protractor';
import { AccountService } from './../_services/account.service';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  @Output() changeRegister= new EventEmitter();
  model: any = {
	};
  errors: string[]= [];
  loginForm: FormGroup;
  constructor(private accountService:AccountService,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }
  initializeForm()
  {
    this.loginForm = this.formBuilder.group({
      username: ['',Validators.required],
      password: ['',[Validators.minLength(3),Validators.maxLength(10) ,Validators.required]],
    });
  }

  login() {
		this.accountService.login(this.loginForm.value).subscribe(res => {
			this.router.navigateByUrl('/members')
		},
    error=>{
      console.log(error);
      this.errors = error;
    })
	}
  register(){
    this.changeRegister.emit(true);
  }
}
