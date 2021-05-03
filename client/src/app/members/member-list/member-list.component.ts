import { take } from 'rxjs/operators';
import { AccountService } from './../../_services/account.service';
import { UserParams } from './../../_models/userParams';
import { Pagination } from './../../_models/pagination';
import { Observable } from 'rxjs';
import { MembersService } from './../../_services/members.service';
import { Member } from './../../_models/member';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members: Member[];
  pagination: Pagination;
  userParams: UserParams;
  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }];
  constructor(private memberService: MembersService) {
    this.userParams = this.memberService.getUserParam();
  }

  ngOnInit(): void {
    this.LoadMembers();
  }
  LoadMembers() {
    this.memberService.setUserParam(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
    });
  }
  resetFilter() {
    this.userParams = this.memberService.resetUserParams();
    this.LoadMembers();
  }
  pageChanged(event) {
    this.userParams.pageNumber = event.page;
    this.memberService.setUserParam(this.userParams);
    this.LoadMembers();
  }
}
