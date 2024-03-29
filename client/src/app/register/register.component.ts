import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister= new EventEmitter();
  errors: string[]= [];
  registerForm: FormGroup;
  maxDate:Date;

  constructor(public accountService: AccountService,
    private formBuilder: FormBuilder,
    private router:Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear()-18);
  }
  initializeForm()
  {
    this.registerForm = this.formBuilder.group({
      gender: ['male'],
      username: ['',Validators.required],
      knownAs: ['',Validators.required],
      dateOfBirth: ['',Validators.required],
      city: ['',Validators.required],
      country: ['',Validators.required],
      password: ['',[Validators.minLength(3),Validators.maxLength(10) ,Validators.required]],
      confirmPassword: ['',[Validators.required,this.matchValues('password')]]
    });
    this.registerForm.controls.password.valueChanges.subscribe(()=>{
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }
  matchValues(matchTo:string):ValidatorFn{
    return (control: AbstractControl)=>{
      return control?.value=== control?.parent?.controls[matchTo].value
      ? null:{isMatching:true}
    }
  }
  register(){
    this.accountService.register(this.registerForm.value).subscribe((res)=>{
        this.router.navigateByUrl('/members');
    },(error)=>{
      this.errors= error;
    })
  }
  cancel(){
    this.cancelRegister.emit(false);
  }
}
