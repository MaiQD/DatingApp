import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister= new EventEmitter();
  model: any = {
  };
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }
  register(){
    this.accountService.register(this.model).subscribe((res)=>{
        console.log(res);
        this.cancel();
    },(error)=>{
      console.log(error);
    })
  }
  cancel(){
    this.cancelRegister.emit(false);
  }
}
