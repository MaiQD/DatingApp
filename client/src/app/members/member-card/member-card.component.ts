import { PresenceService } from './../../_services/presence.service';
import { ToastrService } from 'ngx-toastr';
import { MembersService } from 'src/app/_services/members.service';
import { Member } from './../../_models/member';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member;

  constructor(private membersService: MembersService,
    private toastr: ToastrService,
    public presenceService:PresenceService) { }

  ngOnInit(): void {
  }
  addLike(member: Member) {
    this.membersService.addLike(member.userName).subscribe(() => {
      this.toastr.success('you have liked ' + member.knownAs);
    })

  }
}
