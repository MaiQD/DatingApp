import { transition } from '@angular/animations';
import { User } from './../../_models/user';
import { take } from 'rxjs/operators';
import { AccountService } from './../../_services/account.service';
import { PresenceService } from './../../_services/presence.service';
import { Member } from './../../_models/member';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MembersService } from 'src/app/_services/members.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit,OnDestroy {
  //static true chạy lần đầu tiên khi khởi động trang
  @ViewChild('memberTabs',{static:true}) memberTabs: TabsetComponent;
  member: Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  activeTab: TabDirective
  messages: Message[] = [];
user:User;

  constructor(public presenceService:PresenceService,
    private route: ActivatedRoute,
    private messageService: MessageService,
    private accountService:AccountService,
    private router:Router) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user =>{this.user =user});
      //không bị tái sử dụng route cũ
      this.router.routeReuseStrategy.shouldReuseRoute = ()=>false;
    }
  

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.member = data.member
    });

    this.route.queryParams.subscribe(params => {
      params.tab ? this.selectTab(params.tab) : this.selectTab(0)
    })

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
    this.galleryImages = this.getImages();

  }
  getImages(): NgxGalleryImage[] {
    const imagesUrls = [];
    for (const photo of this.member.photos) {
      imagesUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url,
      })
    }
    return imagesUrls;
  }

  loadMessages() {
    this.messageService.getMessageThread(this.member.userName)
      .subscribe(messages => {
        this.messages = messages;
      });
  }
  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading == "Messages" && this.messages.length == 0) {
      this.messageService.createHubConnection(this.user,this.member.userName);
    }
    else {
      this.messageService.stopHubConnection();
    }
  }
  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }
}
