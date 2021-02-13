import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { MembersService } from 'src/app/_services/members.service';
import { User } from './../../_models/user';
import { Member } from './../../_models/member';
import { AccountService } from './../../_services/account.service';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm
  member: Member;
  user: User;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private accountService: AccountService,
    private memberService: MembersService,
    private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.LoadMember();
  }

  LoadMember() {
    this.memberService.getMember(this.user.username).subscribe(member => this.member = member);
  }
  updateMember() {
    this.memberService.updateMember(this.member).subscribe(()=>{
      this.toastr.success('Profile update success');
      this.editForm.reset(this.member);
    })
    
  }

}
