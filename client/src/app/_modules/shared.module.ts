import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    })
  ],
  //export ra ngoài để những module khai báo module shared này có thể dùng những modules được exports
  exports:[
    BsDropdownModule,
    ToastrModule
  ]
})
export class SharedModule { }
