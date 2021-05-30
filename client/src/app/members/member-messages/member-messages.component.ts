import { NgForm } from '@angular/forms';
import { MessageService } from './../../_services/message.service';
import { Message } from './../../_models/message';
import { ChangeDetectionStrategy, Component, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  changeDetection:ChangeDetectionStrategy.OnPush,
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @ViewChild('messageForm') messageForm:NgForm
  @Input() messages: Message[]=[];
  @Input() username: string;
  content:string;
  constructor(public messageService: MessageService) { }

  ngOnInit(): void {
    
  }
  sendMessage(){
    this.messageService.sendMessage(this.username,this.content ).then(message=>{
      this.messageForm.reset();
    })
  }
}
