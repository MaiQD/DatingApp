import { ConfirmService } from './../_services/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { MessageService } from './../_services/message.service';
import { Pagination } from './../_models/pagination';
import { Message } from './../_models/message';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  pagination: Pagination;
  container = 'Unread';
  pageNumber = 1;
  pageSize = 5;
  loadingFlag=false;

  constructor(private messagesService: MessageService,
    private toastrService:ToastrService,
    private confirmService:ConfirmService) { }

  ngOnInit(): void {
    this.LoadMessages();
  }
  LoadMessages() {
    this.loadingFlag = true;
    this.messagesService.getMessage(this.pageNumber, this.pageSize, this.container).subscribe(response =>{
      this.messages = response.result;
      this.pagination = response.pagination;
      this.loadingFlag=false;
    });
  }
  deleteMessage(id:number){
    this.confirmService.confirm('Confirm delete message', 'This cannot be undone').subscribe(res=>{
      if (res) {
        this.messagesService.deleteMessage(id).subscribe(result=>{
          this.messages.splice(this.messages.findIndex(m=>m.id === id),1);
          this.toastrService.success('Delete success');
        })
      }
    })
    
  }
  pageChanged(event: any) {
    console.log(1);
    this.pageNumber = event.page;
    this.LoadMessages();
  }
}
